/* --------------------------------------------------         
Nivel: un nivel de juego, con su mapa y demás características distintivas
Parte de Fruity Frank - Remake

@see Juego Mapa Nivel1 Nivel2 Nivel3
@author Nacho
       
Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.20  08-Dic-2008  Nacho Cabanes
                    Clase básica: permite dibujar (delegando
                    en el mapa y dibujando luego los personajes),
                    ver si es posible mover a una posición
                    (delegando en el mapa) y ver los puntos
                    obtenidos al mover a una posición (con mapa).
0.24  29-Dic-2008  Nacho Cabanes
                    Añadida una "posicion del nido", para indicar
                    desde donde salen los enemigos "nariz".
0.25  06-Ene-2009  Nacho Cabanes
                    Añadida la posibilidad de reproducir sonidos:
                    cada nivel tiene un sonido de fondo
0.27  06-May-2016  Nacho Cabanes
                    Se puede comprobar colisiones con un rectángulo
                    (mejorado "EsPosibleMover" en la clase Mapa)
---------------------------------------------------- */

using System.Collections.Generic;
/** 
*   
*  
*       
*/
public class Nivel
{

    protected Mapa miMapa;
    protected Enemigo[] enemigos;
    protected List<Manzana> manzanas;
    protected Sonido miMusicaFondo;

    //protected short posXnido;
    //protected short posYnido;

    public Nivel()
    {
        miMapa = new Mapa();
        PrepararManzanas();
    }

    public void PrepararManzanas()
    {
        manzanas = new List<Manzana>();
        for (short fila = 0; fila < miMapa.GetMaxFilas(); fila++)   // Dibujo desde el mapa
            for (short col = 0; col < miMapa.GetMaxColumnas(); col++)
                if ((miMapa.GetPosicion(col, fila)) == 'M')
                {
                    Manzana m = new Manzana(this);
                    int x = col * miMapa.GetAnchoCasilla() + miMapa.GetXIni();
                    int y = fila * miMapa.GetAltoCasilla() + miMapa.GetYIni();
                    m.MoverA(x, y);
                    manzanas.Add(m);
                    miMapa.BorrarPosicion(col, fila);
                }
    }

    public virtual void Dibujar()
    {
        // Dibujo el mapa y los enemigos
        miMapa.Dibujar();
        foreach (Enemigo enem in enemigos)
            enem.Dibujar();
        foreach (Manzana m in manzanas)
            m.Dibujar();
    }

    /// Calcula el siguiente fotograma (animacion) de los elementos del nivel
    public void SiguienteFotograma()
    {
        //Mueve los personajes
        foreach (Enemigo enem in enemigos)
        {
            enem.Mover();
        }
        foreach (Manzana m in manzanas)
            m.Mover();
    }

    /// Indica si es posible mover el personaje a cierta posicion de la 
    /// pantalla (delega en el mapa)
    public bool EsPosibleMoverPersonaje(int x, int y, int xMax, int yMax)
    {
        return miMapa.EsPosibleMoverPersonaje(x, y, xMax, yMax);
    }

    /// Indica si es posible mover una manzana a cierta posicion de la 
    /// pantalla (delega en el mapa)
    public bool EsPosibleMoverManzana(int x, int y, int xMax, int yMax)
    {
        return miMapa.EsPosibleMoverManzana(x, y, xMax, yMax);
    }

    /// Indica si es el enemigo puede moverse a cierta posicion de la pantalla
    public byte MovilidadEnemigo(int x, int y)
    {
        //Delega en el mapa
        return miMapa.MovilidadEnemigo(x, y);
    }

    /// Devuelve los puntos resultantes de moverse a una cierta posicion
    /// y elimina el premio que pudiera haber en esa posicion
    public int PuntosMover(int x, int y)
    {
        //Delega en el mapa
        return miMapa.PuntosMover(x, y);
    }

    /// Borra la casilla de una posicion (X,Y) de pantalla
    public void BorrarPosicionPantalla(int x, int y)
    {
        miMapa.BorrarPosicionPantalla(x, y);
    }

    /// Devuelve si el nivel está completo
    public bool GetCompleto()
    {
        return miMapa.GetNumFrutas() == 0;
    }

    /// Devuelve la lista de enemigos
    public Enemigo[] GetEnemigos()
    {
        return enemigos;
    }

    public byte GetAnchoCasilla()
    {
        return miMapa.GetAnchoCasilla();
    }

    public byte GetAltoCasilla()
    {
        return miMapa.GetAltoCasilla();
    }

    public int GetXSalida()
    {
        return miMapa.GetXSalida();
    }

    public int GetYSalida()
    {
        return miMapa.GetYSalida();
    }

    public void ReproducirMusica()
    {
        if (miMusicaFondo != null)
            miMusicaFondo.ReproducirFondo();
    }

    public void PararMusica()
    {
        if (miMusicaFondo != null)
            miMusicaFondo.Interrumpir();
    }
}