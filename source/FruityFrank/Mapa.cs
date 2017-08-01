/* --------------------------------------------------         
Mapa: el mapa de un nivel
Parte de Fruity Frank - Remake

@see Mapa
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, idéntica a la 0.26
---------------------------------------------------- */

public class Mapa
{
    protected const byte MAXFILAS = 10;
    protected const byte MAXCOLS = 15;
    protected string[] mapa = new string[MAXFILAS];
    protected string[] mapaOriginal = new string[MAXFILAS];

    protected int xNido, yNido;

    // Tamaño de cada "casilla" de pantalla (mapa)
    protected const byte anchoCasilla = 60, altoCasilla = 54;

    // Limites de la pantalla
    protected const int xIniPantalla = (1024-MAXCOLS*anchoCasilla) / 2, 
        xFinPantalla = xIniPantalla + MAXCOLS * anchoCasilla;
    protected const int yIniPantalla = 84, 
        yFinPantalla = yIniPantalla + MAXFILAS * altoCasilla;

    private short numFrutas;

    ElemGrafico fondoNivel1, fondoNivel2, fondoNivel3;
    ElemGrafico platano;
    Cereza cereza;
    ElemGrafico nido;

    /// Constructor: carga las imagenes del mapa
    public Mapa()
    {
        fondoNivel1 = new ElemGrafico("imagenes/fondonivel1.png");
        fondoNivel2 = new ElemGrafico("imagenes/fondonivel2.png");
        fondoNivel3 = new ElemGrafico("imagenes/fondonivel3.png");
        cereza = new Cereza();
        platano = new ElemGrafico("imagenes/platano.png");
        nido = new ElemGrafico("imagenes/nido.png");

        mapaOriginal[0] = "XXXXXXXX XXXXXX";
        mapaOriginal[1] = "XXXXXXXX XXXXXX";
        mapaOriginal[2] = "XXXXXXXX XXXXXX";
        mapaOriginal[3] = "XXXXXXXX XXXXXX";
        mapaOriginal[4] = "               ";
        mapaOriginal[5] = "XXXXXXXX XXXXXX";
        mapaOriginal[6] = "XXXXXXXX XXXXXX";
        mapaOriginal[7] = "XXXXXXXX XXXXXX";
        mapaOriginal[8] = "XXXXXXXX XXXXXX";
        mapaOriginal[9] = "XXXXXXXX XXXXXX";

        Reiniciar();
    }

    /// Devuelve la cantidad de filas del mapa
    public short GetMaxFilas()
    {
        return MAXFILAS;
    }

    /// Devuelve la cantidad de columnas del mapa
    public short GetMaxColumnas()
    {
        return MAXCOLS;
    }

    /// Deja el mapa como era en un principio
    public void Reiniciar()
    {
        for (int i = 0; i < MAXFILAS; i++)
            mapa[i] = mapaOriginal[i];

        for (short fila = 0; fila < MAXFILAS; fila++)   // Dibujo desde el mapa
            for (short col = 0; col < MAXCOLS; col++)
                if ((GetPosicion(col, fila)) == 'N')
                {
                    xNido = xIniPantalla + col * anchoCasilla;
                    yNido = (short)(yIniPantalla + fila * altoCasilla);
                }
    }

    /// Muestra el mapa en pantalla de texto (auxiliar para depuracion)
    public void MostrarComoTexto()
    {
        for (int i = 0; i < MAXFILAS; i++)
        {
            System.Console.WriteLine(mapa[i]);
        }
    }

    /// Devuelve el carácter que hay en la posición [x,y]
    public char GetPosicion(short x, short y)
    {
        try
        {
            return mapa[y][x];
        }
        catch (System.Exception)
        {
            return ' ';
        }
    }

    /// Devuelve la cantidad de frutas del mapa
    public short GetNumFrutas()
    {
        return numFrutas;
    }

    /// Cambia el carácter que hay en la posición [x,y]
    public void SetPosicion(int x, int y, char c)
    {
        mapa[y] = mapa[y].Remove(x, 1);
        mapa[y] = mapa[y].Insert(x, c.ToString());
    }

    /// Borra el carácter que hay en la posición [x,y]
    public void BorrarPosicion(int x, int y)
    {
        SetPosicion(x, y, ' ');
    }

    /// Borra la casilla de una posicion (X,Y) de pantalla
    public void BorrarPosicionPantalla(int x, int y)
    {
        short xMapa = (short)((x - xIniPantalla) / anchoCasilla);
        short yMapa = (short)((y - yIniPantalla) / altoCasilla);

        BorrarPosicion(xMapa, yMapa);
    }

    /// Dibuja el mapa en pantalla
    public void Dibujar()
    {
        numFrutas = 0;
        for (short fila = 0; fila < MAXFILAS; fila++)   // Dibujo desde el mapa
            for (short col = 0; col < MAXCOLS; col++)
            {
                switch (GetPosicion(col, fila))
                {

                    case 'X':   // Fondo de pantalla, nivel 1
                        fondoNivel1.Dibujar(
                        (short)(xIniPantalla + col * anchoCasilla),
                        (short)(yIniPantalla + fila * altoCasilla));
                        break;

                    case 'Y':   // Fondo de pantalla, nivel 2
                        fondoNivel2.Dibujar(
                        (short)(xIniPantalla + col * anchoCasilla),
                        (short)(yIniPantalla + fila * altoCasilla));
                        break;

                    case 'Z':   // Fondo de pantalla, nivel 3
                        fondoNivel3.Dibujar(
                        (short)(xIniPantalla + col * anchoCasilla),
                        (short)(yIniPantalla + fila * altoCasilla));
                        break;

                    case 'C':  // Cerezas
                        cereza.Dibujar(
                        (short)(xIniPantalla + col * anchoCasilla),
                        (short)(yIniPantalla + fila * altoCasilla));
                        numFrutas++;
                        break;

                    /*case 'M':  // Manzanas
                        manzana.Dibujar(
                        (short)(xIniPantalla + col * anchoCasilla),
                        (short)(yIniPantalla + fila * altoCasilla));
                        break;*/

                    case 'N':  // Nido
                        nido.Dibujar(
                        (short)(xIniPantalla + col * anchoCasilla),
                        (short)(yIniPantalla + fila * altoCasilla));
                        break;

                    case 'P':  // Platanos
                        platano.Dibujar(
                        (short)(xIniPantalla + col * anchoCasilla),
                        (short)(yIniPantalla + fila * altoCasilla));
                        numFrutas++;
                        break;
                }
            }
    }

    /// Indica si es posible moverse a cierta posicion de la pantalla
    /*public bool EsPosibleMover(short x, short y)
    {
        short xMapa = (short)((x - xIniPantalla) / anchoCasilla);
        short yMapa = (short)((y - yIniPantalla) / altoCasilla);

        if ((xMapa < 0) || (xMapa >= MAXCOLS) ||  // Si se sale
            (yMapa < 0) || (yMapa >= MAXFILAS)) return false;

        if (GetPosicion(xMapa, yMapa) == 'M')  // Si es manzana
            return false;

        return true;
    }*/

    public bool EsPosibleMoverPersonaje(int xMin, int yMin, int xMax, int yMax)
    {
        if ((xMin < xIniPantalla) || (yMin < yIniPantalla))
            return false;

        if ((xMax > xFinPantalla) || (yMax > yFinPantalla))
            return false;

        for (short fila = 0; fila < MAXFILAS; fila++)
            for (short col = 0; col < MAXCOLS; col++)
            {
                char tileType = GetPosicion(col, fila);
                if (tileType == 'M')
                {
                    int xPos = xIniPantalla + col * anchoCasilla;
                    int yPos = yIniPantalla + fila * altoCasilla;
                    int xLimit = xIniPantalla + (col + 1) * anchoCasilla;
                    int yLimit = yIniPantalla + (fila + 1) * altoCasilla;

                    if (ElemGrafico.ColisionEntre(
                            xMin, yMin, xMax, yMax,
                            xPos, yPos, xLimit, yLimit))
                        return false;
                }
            }

        return true;
    }

    /// 
    /// Comprueba si es posible mover una manzana a ciertas coordenadas
    /// (no será posible si es suelo, fruta o manzana: si hay algo en el
    /// mapa o colisiona con otra manzana)
    /// 
    public bool EsPosibleMoverManzana(int xMin, int yMin, int xMax, int yMax)
    {
        if ((xMin < xIniPantalla) || (yMin < yIniPantalla - altoCasilla))
            return false;

        if ((xMax > xFinPantalla) || (yMax > yFinPantalla))
            return false;

        for (short fila = 0; fila < MAXFILAS; fila++)
            for (short col = 0; col < MAXCOLS; col++)
            {
                char tileType = GetPosicion(col, fila);
                if ((tileType == 'X') || (tileType == 'Y') || (tileType == 'Z')
                        || (tileType == 'C') || (tileType == 'P'))
                    {
                    int xPos = xIniPantalla + col * anchoCasilla;
                    int yPos = yIniPantalla + fila * altoCasilla;
                    int xLimit = xIniPantalla + (col + 1) * anchoCasilla;
                    int yLimit = yIniPantalla + (fila + 1) * altoCasilla;

                    if (ElemGrafico.ColisionEntre(
                            xMin, yMin, xMax, yMax,
                            xPos, yPos, xLimit, yLimit))
                        return false;
                }
            }
        // If we have not collided with anything... then we can move
        return true;
    }


    /// Indica si es el enemigo puede moverse a cierta posicion de la pantalla
    /// (2 = sí, hueco; 1 = a veces, pared; 0 = nunca, fruta).
    public byte MovilidadEnemigo(int x,int y)
    {
        short xMapa = (short)((x - xIniPantalla) / anchoCasilla);
        short yMapa = (short)((y - yIniPantalla) / altoCasilla);

        if ((x < xIniPantalla) || (xMapa >= MAXCOLS) ||  // Si se sale, no puede
            (y < yIniPantalla) || (yMapa >= MAXFILAS)) return 0;

        char simbolo = GetPosicion(xMapa, yMapa);

        if ((simbolo == ' ')     // Si es hueco, sí puede
            || (simbolo == 'N'))  // También si es el nido
            return 2;

        if ((simbolo == 'X')  // Si es pared, puede con dificultad
            || (simbolo == 'Y') || (simbolo == 'Z'))
            return 1;

        // En el resto de casos, sería una fruta y no puede
        return 0;
    }


    /// Devuelve los puntos resultantes de moverse a una cierta posicion
    /// y elimina el premio que pudiera haber en esa posicion
    public int PuntosMover(int x, int y)
    {
        short xMapa = (short)((x - xIniPantalla) / anchoCasilla);
        short yMapa = (short)((y - yIniPantalla) / altoCasilla);

        if (GetPosicion(xMapa, yMapa) == 'C')  // Si es cereza
        {
            BorrarPosicion(xMapa, yMapa);
            return 10;
        }

        if (GetPosicion(xMapa, yMapa) == 'P')  // Si es platano
        {
            BorrarPosicion(xMapa, yMapa);
            return 20;
        }

        if ((GetPosicion(xMapa, yMapa) == 'X') ||  // Fondo: borra sin puntos
            (GetPosicion(xMapa, yMapa) == 'Y') ||
            (GetPosicion(xMapa, yMapa) == 'Z'))
        {
            BorrarPosicion(xMapa, yMapa);
        }

        return 0;
    }


    /// Devuelve el ancho de cada casilla del mapa 
    public byte GetAnchoCasilla()
    {
        return anchoCasilla;
    }

    /// Devuelve el alto de cada casilla del mapa 
    public byte GetAltoCasilla()
    {
        return altoCasilla;
    }

    /// Devuelve la coordenada X (columna) inicial de la pantalla
    public int GetXIni()
    {
        return xIniPantalla;
    }

    /// Devuelve la coordenada Y (fila) inicial de la pantalla
    public int GetYIni()
    {
        return yIniPantalla;
    }

    /// Devuelve la coordenada X (columna) del nido de los enemigos
    public int GetXNido()
    {
        return xNido;
    }

    /// Devuelve la coordenada Y (fila) del nido de los enemigos
    public int GetYNido()
    {
        return yNido;
    }

    public int GetXSalida()
    {
        return xNido;
    }

    public int GetYSalida()
    {
        return yFinPantalla-altoCasilla;
    }

}
