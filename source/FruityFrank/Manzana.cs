/* --------------------------------------------------         
Manzana: uno de los tipos de elementos graficos del juego
Parte de Fruity Frank - Remake

@see ElemGrafico Jueo
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, idéntica a la 0.26
---------------------------------------------------- */

public class Manzana : ElemGrafico
{
    Nivel miNivel;

    public Manzana(Nivel n)
    {
        CargarImagen("imagenes/manzana.png");
        miNivel = n;
        incrY = 3;
    }

    /// Mueve la manzana hacia abajo, en caso de que sea posible. Comprueba
    /// si choca con algún enemigo o con el personaje.
    public override void Mover()
    {
        if (miNivel.EsPosibleMoverManzana(x, y + incrY,
            (short)(x + alto), (short)(y + incrY + alto)))
        {
            y += incrY;
        }
        // TO DO: Chocar con enemigos
    }

}
