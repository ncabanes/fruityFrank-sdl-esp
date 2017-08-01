/* --------------------------------------------------         
Pepino: uno de los tipos de elementos graficos del juego (uno de enemigos)
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

public class Pepino : Enemigo
{

    bool parado;
    short incrXhabitual, incrYhabitual;

    public Pepino(Nivel n)  // Constructor: carga la imagen por defecto
    {
        CargarSecuencia(IZQUIERDA,
          new string[] {"imagenes/enemigo1i1.png",
          "imagenes/enemigo1i2.png"});
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
                (short)(x + miNivel.GetAnchoCasilla()), y) >= 1)
            {
                x += incrX;
                miNivel.BorrarPosicionPantalla(x, y);
            }
            else
                parado = true;
        }

        if ((!parado) && (incrX < 0))
        {
            if (miNivel.MovilidadEnemigo(
                (short)(x + incrX), y) >= 1)
            {
                x += incrX;
                miNivel.BorrarPosicionPantalla(x, y);
            }
            else
                parado = true;
        }

        if ((!parado) && (incrY > 0))
        {
            if (miNivel.MovilidadEnemigo(
                x, (short)(y + miNivel.GetAltoCasilla())) >= 1)
            {
                y += incrY;
                miNivel.BorrarPosicionPantalla(x, y);
            }
            else
                parado = true;
        }
        if ((!parado) && (incrY < 0))
        {
            if (miNivel.MovilidadEnemigo(
                 x, (short)(y + incrY)) >= 1)
            {
                y += incrY;
                miNivel.BorrarPosicionPantalla(x, y);
            }
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
          && (miNivel.MovilidadEnemigo(x, (short)(y - miNivel.GetAltoCasilla())) >= 1))
        {
            incrY = (short)(-incrYhabitual);
            parado = false;
            return;
        }
        if ((nuevaDireccion == 2) // Abajo
          && (miNivel.MovilidadEnemigo(x, (short)(y + miNivel.GetAltoCasilla())) >= 1))
        {
            incrY = incrYhabitual;
            parado = false;
            return;
        }
        if ((nuevaDireccion == 3) // Derecha
          && (miNivel.MovilidadEnemigo((short)(x + miNivel.GetAnchoCasilla()), y) >= 1))
        {
            incrX = incrXhabitual;
            parado = false;
            return;
        }
        if ((nuevaDireccion == 4) // Izquierda
          && (miNivel.MovilidadEnemigo((short)(x - miNivel.GetAnchoCasilla()), y) >= 1))
        {
            incrX = (short)(-incrXhabitual);
            parado = false;
            return;
        }
    }
}
