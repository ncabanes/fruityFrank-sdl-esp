/* --------------------------------------------------         
ElemGrafico: elemento grafico (sprite)   
Parte de Fruity Frank - Remake

@see Hardware Juego Imagen
@author  Nacho

Versiones:
   
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, casi idéntica a la 0.27,
 ---------------------------------------------------- */


public class ElemGrafico
{
    // ----- Atributos -----
    protected int x;
    protected int y;
    protected int incrX;
    protected int incrY;
    protected int alto;
    protected int ancho;
    protected bool activo;

    protected int xOriginal;  // Para llevar a su posicion inicial
    protected int yOriginal;

    // La imagen que se mostrará en pantalla, si es estatica
    protected Imagen miImagen;

    // La secuencia de imagenes, si es animada
    protected Imagen[][] secuencia;
    protected byte fotogramaActual;
    protected byte direccion;
    public const byte ABAJO = 0;
    public const byte ARRIBA = 1;
    public const byte DERECHA = 2;
    public const byte IZQUIERDA = 3;

    bool contieneImagen;        // Si no contiene imagen, no se podra dibujar
    bool contieneSecuencia;     // La alternativa: imagenes multiples

    protected Juego miJuego;    // Para comunicar con el resto de elementos

    // frame actual, para usar con "framesPorFotograma"
    protected int contadorFrame;
    // Frames de juego hasta cambiar de fotograma
    protected int framesPorFotograma;

    // ----- Operaciones -----

    /// Constructor vacio
    public ElemGrafico()
    {
        contieneImagen = false;
        contieneSecuencia = false;
        activo = true;
        direccion = ABAJO;
        fotogramaActual = 0;
        secuencia = new Imagen[4][];
        framesPorFotograma = 1;

        // Particular para Fruity Frank
        ancho = 60;
        alto = 54;
    }

    /// Constructor: Carga la imagen que representara a este elemento grafico
    public ElemGrafico(string nombre)
    {
        CargarImagen(nombre);
        direccion = ABAJO;
        activo = true;
        fotogramaActual = 0;
    }

    /// Mueve el elemento grafico a otra posicion
    /// Define la posicion original, si no tenia valor
    public void MoverA(int nuevaX, int nuevaY)
    {
        x = nuevaX;
        y = nuevaY;
        if ((xOriginal == 0) && (yOriginal == 0))
        {
            xOriginal = nuevaX;
            yOriginal = nuevaY;
        }
    }

    /// Cambia la velocidad (incrX e incrY) de un elemento
    public void SetVelocidad(short vX, short vY)
    {
        incrX = vX;
        incrY = vY;
    }

    /// Carga la imagen que representara a este elemento grafico
    public void CargarImagen(string nombre)
    {
        miImagen = new Imagen();
        miImagen.Cargar(nombre);
        contieneImagen = true;
        contieneSecuencia = false;
    }

    /// Carga una secuencia de imagenes para un elemento animado
    public void CargarSecuencia(byte direcc, string[] nombres)
    {
        contieneImagen = true;
        contieneSecuencia = true;
        byte tamanyo = (byte)nombres.Length;
        secuencia[direcc] = new Imagen[tamanyo];
        for (byte i = 0; i < nombres.Length; i++)
        {
            secuencia[direcc][i] = new Imagen(nombres[i]);
        }
    }

    /// Mueve el elemento grafico a otra posicion
    public void CambiarDireccion(byte nuevaDir)
    {
        if (direccion != nuevaDir)
        {
            direccion = nuevaDir;
            fotogramaActual = 0;
        }
    }

    /// Devuelve el personaje a su posición inicial
    public void Reiniciar()
    {
        x = xOriginal;
        y = yOriginal;
    }


    /// Dibuja el elemento grafico en su posicion actual
    public void Dibujar()
    {
        if (activo == false) return;
        if (contieneSecuencia)
            secuencia[direccion][fotogramaActual].Dibujar(x, y);
        else if (contieneImagen)
            miImagen.Dibujar(x, y);
        else
            Hardware.ErrorFatal("Se ha intentado dibujar una imagen no cargada!");
    }

    /// Dibuja el elemento grafico en una posicion cualquiera
    public void Dibujar(short nuevaX, short nuevaY)
    {
        MoverA(nuevaX, nuevaY);
        Dibujar();
    }

    /// Comprueba si ha chocado con otro elemento gráfico
    public bool ColisionCon(ElemGrafico otroElem)
    {
        // No se debe chocar con un elemento oculto      
        if ((activo == false) || (otroElem.activo == false))
            return false;
        // Ahora ya compruebo coordenadas
        if ((otroElem.x + otroElem.ancho > x)
                && (otroElem.y + otroElem.alto > y)
                && (x + ancho > otroElem.x)
                && (y + alto > otroElem.y))
            return true;
        else
            return false;
    }

    /// Comprueba si ha chocado con un rectángulo delimitado por
    /// ciertas coordenadas
    public bool ColisionCon(int xIni, int yIni, int xFin, int yFin)
    {
        if (activo &&
                (x < xFin) &&
                (x + ancho > xIni) &&
                (y < yFin) &&
                (y + alto > yIni)
                )
            return true;
        return false;
    }

    /// Comprueba colisión entre dos rectángulos
    public static bool ColisionEntre(
        int r1xStart, int r1yStart, int r1xEnd, int r1yEnd,
        int r2xStart, int r2yStart, int r2xEnd, int r2yEnd)
    {
        if ((r2xStart < r1xEnd) &&
                (r2xEnd > r1xStart) &&
                (r2yStart < r1yEnd) &&
                (r2yEnd > r1yStart)
                )
            return true;
        else
            return false;
    }

    /// Prepara el siguiente fotograma, para animar el movimiento de
    /// un personaje
    public void SiguienteFotograma()
    {
        contadorFrame++;
        if (contadorFrame >= framesPorFotograma)
        {
            contadorFrame = 0;
        
            if (fotogramaActual < secuencia[direccion].Length - 1)
                fotogramaActual++;
            else
                fotogramaActual = 0;
        }
    }

    public virtual void Mover()
    {
        // Para ser redefinido en las clases "hijas"
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

}
