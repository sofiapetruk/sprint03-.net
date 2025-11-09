using System.Text.Json;
using Microsoft.ML;

namespace Sprint02.NovaPasta
{
    public class TreinamentoService
    {

        private readonly string _modeloPath = "data/modelo_treinado.zip";

        private readonly MLContext _mlContext;

        private ITransformer? _modelo;

        private PredictionEngine<DadosMotos, PrevisaoMotos>? _predEngine;

        public TreinamentoService()
        {
            _mlContext = new MLContext();

            TreinarModelo();
        }

        private void TreinarModelo()
        {
            string _jsonPath = "data/dadosTreinamento.json";

            string jsonString = File.ReadAllText(_jsonPath);

            List<DadosMotos>? dadosJson = JsonSerializer.Deserialize<List<DadosMotos>>(
                jsonString
            );

            var dados = _mlContext.Data.LoadFromEnumerable(dadosJson);

            var pipeline = _mlContext.Transforms.Conversion.ConvertType(
                outputColumnName: "Label",
                inputColumnName: nameof(DadosMotos.MotoEscolhida),
                outputKind: Microsoft.ML.Data.DataKind.Boolean)

                .Append(_mlContext.Transforms.Categorical.OneHotHashEncoding(
                    outputColumnName: "UsoPrincipalEncoded",
                    inputColumnName: nameof(DadosMotos.UsoPrincipal)))

                .Append(_mlContext.Transforms.Concatenate("Features",
                                                          nameof(DadosMotos.Idade),
                                                          nameof(DadosMotos.RendaMensal),
                                                          nameof(DadosMotos.PreferenciaEconomia),
                                                          nameof(DadosMotos.PreferenciaPotencia),
                                                          "UsoPrincipalEncoded")) 

                .Append(_mlContext.Transforms.NormalizeMinMax("Features", "Features"))

                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                    labelColumnName: "Label",
                    featureColumnName: "Features"));

            _modelo = pipeline.Fit(dados);

            _mlContext.Model.Save(_modelo, dados.Schema, _modeloPath);

            if (_modelo != null)
            {
                _predEngine = _mlContext.Model.CreatePredictionEngine<DadosMotos, PrevisaoMotos>(_modelo);
            }

        }

        public PrevisaoMotos Predict(DadosMotos input)
        {
            if (_predEngine == null)
            {
                throw new InvalidOperationException("Verifique se o modelo foi treinado/carregado");
            }
            return _predEngine.Predict(input);
        }

        public void Dispose()
        {
            _predEngine?.Dispose();
            _modelo = null;
        }
    }
}
