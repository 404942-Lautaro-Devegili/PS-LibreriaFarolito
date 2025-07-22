namespace LibreriaBack.Datos
{
    public class Parametro
    {
        public string Nombre { get; set; }

        public object Valor { get; set; }

        public Parametro(string nombre, object valor)
        {
            Nombre = nombre;
            Valor = valor;
        }
        public Parametro()
        {
            Nombre = "";
            Valor = 0;
        }
    }
}
