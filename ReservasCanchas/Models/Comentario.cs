



namespace ReservasCanchas.Models;

    public class Comentario
    {

    public int IDComentario { get; set; } 

    public int IDUsuario { get; set; }

   
    public int IDCancha { get; set; }

    public string Contenido { get; set; } 

    public DateTime Fecha { get; set; } = DateTime.Now;
    public UserComment usuarios { get; set; } 

}

public class UserComment
{
    public string Nombre { get; set; }
    public int IDUsuario { get; set; }

}

