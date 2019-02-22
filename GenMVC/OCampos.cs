namespace GenMVC
{
    public class OCampos
    {
        public string Campo { set; get; } = string.Empty;
        public string TipoSql { set; get; } = string.Empty;
        public string TipoDotNet { set; get; } = string.Empty;
        public int Largo { set; get; } = 0;
        public int Precision { set; get; } = 0;
        public int Escala { set; get; } = 0;
        public bool Where { set; get; } = false;
    }
}