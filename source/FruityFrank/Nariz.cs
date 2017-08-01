/* --------------------------------------------------         
Nariz: uno de los tipos de elementos graficos del juego (uno de los enemigos)
Parte de Fruity Frank - Remake

@see ElemGrafico
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, idéntica a la 0.24
---------------------------------------------------- */

using System;

public class Nariz : Enemigo
{

    bool parado;
    short incrXhabitual, incrYhabitual;

    public Nariz(Nivel n)  // Constructor: carga la imagen por defecto
    {
        CargarSecuencia(IZQUIERDA,
          new string[] {"imagenes/enemigo2i1.png",
          "imagenes/enemigo2i2.png"});
        direccion = IZQUIERDA;
        miNivel = n;
        incrX = 0; incrY = 0;
        activo = false;
        parado = true;
        incrXhabitual = 4;
        incrYhabitual = 3;
    }

    public override void Mover()
    {
        // Si no está activo, espero el tiempo estipulado para que aparezca
        if (!activo)
        {
            contadorHastaRetardo++;
            if (contadorHastaRetardo >= retardo)
                activo = true;
            return;
        }

        // Si está parado, busco nueva dirección
        if (parado)
        {
            calcularNuevaDireccion();
            return;
        }

        // Si se puede mover en horizontal o vertical, avanza
        if ((!parado) && (incrX > 0))
        {
            if (miNivel.MovilidadEnemigo(
                (short)(x + miNivel.GetAnchoCasilla()), y) == 2)
                x += incrX;
            else
                parado = true;
        }

        if ((!parado) && (incrX < 0))
        {
            if (miNivel.MovilidadEnemigo(
                (short)(x + incrX), y) == 2)
                x += incrX;
            else
                parado = true;
        }

        if ((!parado) && (incrY > 0))
        {
            if (miNivel.MovilidadEnemigo(
                x, (short)(y + miNivel.GetAltoCasilla())) == 2)
                y += incrY;
            else
                parado = true;
        }
        if ((!parado) && (incrY < 0))
        {
            if (miNivel.MovilidadEnemigo(
                 x, (short)(y + incrY)) == 2)
                y += incrY;
            else
                parado = true;
        }

        SiguienteFotograma();
    }

    private void calcularNuevaDireccion()
    {
        incrX = 0; incrY = 0;
        Random r = new Random(DateTime.Now.Millisecond);
        // Elijo una dirección al  azar
        int nuevaDireccion = r.Next(1, 5);

        // Y compruebo si en esa dirección hay hueco
        if ((nuevaDireccion == 1) // Arriba
          && (miNivel.MovilidadEnemigo(x, (short)(y - miNivel.GetAltoCasilla())) == 2))
        {
            incrY = (short)(-incrYhabitual);
            parado = false;
            return;
        }
        if ((nuevaDireccion == 2) // Abajo
          && (miNivel.MovilidadEnemigo(x, (short)(y + miNivel.GetAltoCasilla())) == 2))
        {
            incrY = incrYhabitual;
            parado = false;
            return;
        }
        if ((nuevaDireccion == 3) // Derecha
          && (miNivel.MovilidadEnemigo((short)(x + miNivel.GetAnchoCasilla()), y) == 2))
        {
            incrX = incrXhabitual;
            parado = false;
            return;
        }
        if ((nuevaDireccion == 4) // Izquierda
          && (miNivel.MovilidadEnemigo((short)(x - miNivel.GetAnchoCasilla()), y) == 2))
        {
            incrX = (short)(-incrXhabitual);
            parado = false;
            return;
        }

        // Si no habia hueco, genero una probabilidad de romper
        //int probabilidadRomper = r.Next(1, 5);    
        // Y ahora, si el número generado es 1 (25% de probabilidad)
        // habría que repetir lo mismo, pero comprobando si es una pared
    }

}
