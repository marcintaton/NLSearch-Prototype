using System.Text.Json;
using NLSearchWeb.src.NLSE.Model;
using NLSearchWeb.src.NLSE.PreModel;
using NLSearchWeb.src.Utilities;

namespace NLSearchWeb.src.NLSE
{
    public class NLSEngine
    {
        public async Task<string> ProcessQuery(string query)
        {
            // parsing
            var nlp = new NLP();

            var poi = nlp.GetPointsOfInterest(query);

            foreach (var p in poi)
            {
                Console.WriteLine(p);
            }

            if (poi.Count == 0) return "";

            // translation
            // could be done with one request - up to 10 words (I think)
            var tokenTranslations = new List<TokenTranslation>();

            foreach (var p in poi)
            {
                var t = new TokenTranslation(p);
                await t.Translate();

                tokenTranslations.Add(t);
            }

            // pre model
            var preModel = new PreModelDef();

            // totally arbitral number - has a huge impast on how query works
            var maxDiff = 2;

            foreach (var tt in tokenTranslations)
            {
                var inserted = false;

                foreach (var t in tt.wordList)
                {
                    for (int i = 0; i < DbHelper.tables.Count; i++)
                    {
                        if (DamerauLevenshtein.GetDistance(t, DbHelper.tables[i]) <= maxDiff)
                        {
                            inserted = true;
                            preModel.tables.Add(new TokenToTable(t, DbHelper.tables[i]));
                        }

                        foreach (var c in DbHelper.columns[i])
                        {
                            if (DamerauLevenshtein.GetDistance(t, c) <= maxDiff)
                            {
                                inserted = true;

                                preModel.columns.Add(new TokenToColumn(t, c, DbHelper.tables[i]));
                            }
                        }
                    }
                }


                if (!inserted)
                {
                    foreach (var t in tt.wordList)
                    {
                        preModel.values.Add(new TokenToValue(t, t));
                    }
                }
            }

            preModel.Sort();

            Console.WriteLine("\nTables");
            foreach (var x in preModel.tables)
            {
                Console.WriteLine(x._token + " " + x._tableName + " " + x._dlDistance);
            }

            Console.WriteLine("\nColumns");
            foreach (var x in preModel.columns)
            {
                Console.WriteLine(x._token + " " + x._tableName + " " + x._columnName + " " + x._dlDistance);
            }

            Console.WriteLine("\nValues");
            foreach (var x in preModel.values)
            {
                Console.WriteLine(x._value);
            }


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
                Translations = tokenTranslations.ToArray(),
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