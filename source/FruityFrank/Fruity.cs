/* --------------------------------------------------         
Fruity: clase que contiene Main y lanza el juego
Parte de Fruity Frank - Remake

@see Juego
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Desglosada desde Juego
---------------------------------------------------- */

public class Fruity
{
    /// Main: lanza el juego
    public static void Main(string[] args)
    {
        Juego juego = new Juego();

        do
        {
            // Después, pantalla de presentacion
            juego.LanzarPresentacion();
            // Y luego, la juego en sí
            if (!juego.EsFinDelJuego())
                juego.BuclePrincipal();
        } while (!juego.EsFinDelJuego());

    }
}
