using Microsoft.ML.Data;

namespace Sprint02.NovaPasta
{
    public class PrevisaoMotos
    {
        [ColumnName("Probability")]
        public float Probabilidade { get; set; }

        [ColumnName("PredictedLabel")]
        public bool PrevisaoMoto { get; set; }
    }
}
