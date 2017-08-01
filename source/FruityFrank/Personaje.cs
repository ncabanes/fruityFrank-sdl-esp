/* --------------------------------------------------         
Personaje: uno de los tipos de elementos graficos del juego
Parte de Fruity Frank - Remake

@see ElemGrafico
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.20  08-Dic-2008  Nacho Cabanes
                    Clase básica permite mover en las 4 direcciones
                    (mirando si se puede y aumentando marcador),
                    y constructor que fija imagen y atributos
                    básicos y asocia con Juego.
0.23  19-Dic-2008  Nacho Cabanes
                    El movimiento es más suave: cambiados MoverDerecha
                    y similares, añadido Mover
0.27  01-May-2016  Nacho Cabanes
                    Comprobación de colisiones más detallada (por rectángulo,
                    en vez de sólo por esquina final)
0.28  06-May-2016  Nacho Cabanes
                    Retabulado a 4 espacios. Renombrado "PosibleMover" a 
                    "PosibleMoverPersonaje". Comentarios más detallados.

---------------------------------------------------- */

public class Personaje : ElemGrafico
{
    short numInicialVidas;
    short numVidas;

    int anchoCasilla, altoCasilla;  // Para comparar cuando se mueve poco a poco
    int incrXActual, incrYActual;   // Para saber cuanto he acanzado al pulsar una tecla
    int xFinal, yFinal;
    bool enMovimiento;


    public Personaje(Juego j)  // Constructor
    {
        miJuego = j;
        // El personaje se moverá de varios en varios pixeles
        // cuando se pulse cada tecla
        anchoCasilla =ancho;
        altoCasilla = alto;
        incrX = 6;
        incrY = 6;
        enMovimiento = false;
        // Posicion inicial: la primera casilla del mapa
        x = 12;
        y = 82;
        xOriginal = 0;
        yOriginal = 0;
        // Vidas
        numInicialVidas = 3;
        numVidas = 3;

        // Si fuera una imagen estática, se haría con:
        // CargarImagen("imagenes/personajea1.png");

        CargarSecuencia(ARRIBA,
            new string[] {"imagenes/personajea1.png",
            "imagenes/personajea2.png"});
        CargarSecuencia(ABAJO,
            new string[] {"imagenes/personajea1.png",
            "imagenes/personajea2.png"});
        CargarSecuencia(DERECHA,
            new string[] {"imagenes/personajed1.png",
            "imagenes/personajed2.png"});
        CargarSecuencia(IZQUIERDA,
            new string[] {"imagenes/personajei1.png",
            "imagenes/personajei2.png"});

        framesPorFotograma = 2;
    }


    /// Continúa el movimiento (si corresponde) hasta la posición en la que
    /// deba detenerse
    public new void Mover()
    {
        if (!enMovimiento)  // Si no está en movimiento, no hay que hacer nada
            return;
        SiguienteFotograma();
        x += incrXActual;      // Aumento otro poco la posición
        y += incrYActual;
        // Compruebo si me paso (ancho y salto pueden no ser proporcionales)
        if ((incrXActual > 0) && (x >= xFinal))
            x = xFinal;
        if ((incrXActual < 0) && (x <= xFinal))
            x = xFinal;
        if ((incrYActual > 0) && (y >= yFinal))
            y = yFinal;
        if ((incrYActual < 0) && (y <= yFinal))
            y = yFinal;
        // Compruebo si ya he avanzado toda la casilla, para dejar de mover  
        if ((x == xFinal) && (y == yFinal))
            enMovimiento = false;
    }

    /// Comienza una secuencia de movimiento hacia la derecha, en caso de que
    /// no haya un obstáculo que lo impida (el movimiento seguirá hasta recorrer
    /// toda la siguiente casilla).
    public void MoverDerecha()
    {
        if (enMovimiento)
            return;
        if (miJuego.GetNivelActual().EsPosibleMoverPersonaje(
          (short)(x + ancho), y, (short)(x + ancho * 2), (short)(y + alto)))
        {
            CambiarDireccion(DERECHA);
            enMovimiento = true;
            incrXActual = incrX; incrYActual = 0;
            xFinal = (short)(x + anchoCasilla); yFinal = y;
            miJuego.GetMarcador().IncrPuntuacion(
              miJuego.GetNivelActual().PuntosMover((short)(x + anchoCasilla), y));
        }
    }

    /// Comienza una secuencia de movimiento hacia la izquierda, en caso de que
    /// no haya un obstáculo que lo impida (el movimiento seguirá hasta recorrer
    /// toda la siguiente casilla).
    public void MoverIzquierda()
    {
        if (enMovimiento)
            return;
        if (miJuego.GetNivelActual().EsPosibleMoverPersonaje(
         (short)(x - ancho), y, x, (short)(y + alto)))
        {
            CambiarDireccion(IZQUIERDA);
            enMovimiento = true;
            incrXActual = (short)-incrX; incrYActual = 0;
            xFinal = (short)(x - anchoCasilla); yFinal = y;
            miJuego.GetMarcador().IncrPuntuacion(
              miJuego.GetNivelActual().PuntosMover((short)(x - anchoCasilla), y));
        }
    }


    /// Comienza una secuencia de movimiento hacia arriba, en caso de que
    /// no haya un obstáculo que lo impida (el movimiento seguirá hasta recorrer
    /// toda la siguiente casilla).
    public void MoverArriba()
    {
        if (enMovimiento)
            return;
        if (miJuego.GetNivelActual().EsPosibleMoverPersonaje(
          x, (short)(y - alto), (short)(x + ancho), y))
        {
            CambiarDireccion(ARRIBA);
            enMovimiento = true;
            incrXActual = 0; incrYActual = (short)(-incrY);
            xFinal = x; yFinal = (short)(y - altoCasilla);
            miJuego.GetMarcador().IncrPuntuacion(
              miJuego.GetNivelActual().PuntosMover(x, (short)(y - altoCasilla)));
        }
    }

    /// Comienza una secuencia de movimiento hacia abajo, en caso de que
    /// no haya un obstáculo que lo impida (el movimiento seguirá hasta recorrer
    /// toda la siguiente casilla).
    public void MoverAbajo()
    {
        if (enMovimiento)
            return;
        if (miJuego.GetNivelActual().EsPosibleMoverPersonaje(
          x, (short)(y + alto), (short)(x + ancho), (short)(y + alto * 2)))
        {
            CambiarDireccion(ABAJO);
            enMovimiento = true;
            incrXActual = 0; incrYActual = incrY;
            xFinal = x; yFinal = (short)(y + altoCasilla);
            miJuego.GetMarcador().IncrPuntuacion(
              miJuego.GetNivelActual().PuntosMover(x, (short)(y + altoCasilla)));
        }
    }

    /// Al perder una vida: se decuenta marcador, se muestra mensaje de aviso
    /// y se reinicia al jugador en la posición de partida del nivel.
    public void PerderVida()
    {
        numVidas--;
        miJuego.MensajeAviso("Has perdido una vida!", 2000);
        Reiniciar();
    }

    public int GetNumVidas()
    {
        return numVidas;
    }

    public void SetNumVidas(short n)
    {
        numVidas = n;
    }

    /// Recolocar al jugador en su posición de partida para este nivel.
    public new void Reiniciar()
    {
        x = xOriginal;
        y = yOriginal;
        enMovimiento = false;
    }

}