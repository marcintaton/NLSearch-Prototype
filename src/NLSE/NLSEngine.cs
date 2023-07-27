using System.Text.Json;
using NLSearchWeb.src.NLSE.Model;
using NLSearchWeb.src.NLSE.PreModel;
using NLSearchWeb.src.Utilities;
using NLSearchWeb.src.Utilities.DB;

namespace NLSearchWeb.src.NLSE
{
    public class NLSEngine
    {
        public async Task<string> ProcessQuery(string query)
        {
            Console.WriteLine("\n----------------------------------");

            await DbHelper.Init();

            // parsing
            var nlp = new NLP();

            var poi = await nlp.GetPointsOfInterest(query);

            if (poi.Count == 0) return "";

            // pre model
            var preModel = new PreModelDef();

            foreach (var p in poi)
            {
                var tables = DbHelper.CompareTables(p, nlp.lastLang.ToString());
                var columns = DbHelper.CompareColumns(p, nlp.lastLang.ToString());

                // ---------------------------------------------------------------------------
                // absolutely arbitrary magic number with huge impact,
                // that filters garbage from actual results.
                // Should be replaced with a proper, more complicated heuristic 
                var similarityFilter = 0.7f;
                var filteredTables = tables.Where(x => x._similarity >= similarityFilter).ToList();
                var filteredColumns = columns.Where(x => x._similarity >= similarityFilter).ToList();
                // ---------------------------------------------------------------------------

                preModel.tables.AddRange(filteredTables);
                preModel.columns.AddRange(filteredColumns);


                if (filteredTables.Count == 0 && filteredColumns.Count == 0)
                {
                    preModel.values.Add(new TokenToValue(p, p));
                }
            }

            preModel.Sort();

            // ----------------------------------------------------------------
            // show pre-model results
            Console.WriteLine("\nTables");
            foreach (var x in preModel.tables)
            {
                Console.WriteLine("Token: " + x._token + " :: Table: " + x._tableName + ", Similarity: " + x._similarity);
            }
            Console.WriteLine("\nColumns");
            foreach (var x in preModel.columns)
            {
                Console.WriteLine("Token: " + x._token + " :: Table: " + x._tableName + ", Column: " + x._columnName + ", Similarity: " + x._similarity);
            }
            Console.WriteLine("\nValues");
            foreach (var x in preModel.values)
            {
                Console.WriteLine("Token: " + x._token + " :: Value: " + x._value);
            }
            // ----------------------------------------------------------------


            // model
            var models = new List<Model.Model>();

            foreach (var t in preModel.tables)
            {
                var cols = preModel.columns.FindAll(x => x._tableName == t._tableName);

                foreach (var c in cols)
                {
                    foreach (var v in preModel.values)
                    {
                        var model = new Model.Model();
                        model.binds.Add(new ModelBind
                        {
                            table = t._tableName,
                            column = c._columnName,
                            value = v._value
                        });

                        models.Add(model);
                    }
                }
            }

            var sqlQueries = new List<string>();

            foreach (var m in models)
            {
                sqlQueries.Add(QueryBuilder.Build(m));
            }


            // prep result
            var result = new QueryResult
            {
                Translations = null,
                sqlQueries = sqlQueries.ToArray()
            };

            // return data
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            string jsonString = JsonSerializer.Serialize(result, jso);

            return jsonString;
        }
    }
}