/* --------------------------------------------------         
Marcador: muestra la puntuacion del juego
Parte de Fruity Frank - Remake

@see Imagen Fuente ElemGrafico
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, casi idéntica a la 0.24 de 2008,
                   adaptada a 1024x768 en vez de 640x480
---------------------------------------------------- */

public class Marcador
{
    // Atributos

    private int puntuacion;
    private int mejorPunt;

    private Fuente fuenteSans;
    private ElemGrafico iconoVida;

    private Juego miJuego;    // Para comunicar con el resto de elementos

    // Operaciones

    /// Constructor
    public Marcador(Juego j)
    {
        fuenteSans = new Fuente("FreeSansBold.ttf", 18);
        iconoVida = new ElemGrafico("imagenes/personajed1.png");
        miJuego = j;
        mejorPunt = 0;
    }

    /// Lee el valor actual de la puntuación
    public int GetPuntuacion()
    {
        return puntuacion;
    }

    /// Modifica el valor actual de la puntuación
    public void SetPuntuacion(int valor)
    {
        puntuacion = valor;
    }

    /// Comprueba si una puntuacion es un nuevo record,
    /// y actualiza el record si lo es
    public void CompruebaRecord()
    {
        if (puntuacion > mejorPunt)   // Y compruebo si es record
            mejorPunt = puntuacion;
    }

    /// Incrementa el valor actual de la puntuación
    public void IncrPuntuacion(int valor)
    {
        puntuacion += valor;
    }

    /// Dibuja el marcador en pantalla
    public void Dibujar()
    {
        // Tabla de records y vidas restantes
        Hardware.RectanguloRGBA(60, 35, 900, 80, // Marco para records
          255, 109, 8,  // Naranja
          255); // Opaco
        Hardware.EscribirTextoOculta("Puntos", 60, 60,
          0x88, 0xFF, 0xFF, fuenteSans);
        Hardware.EscribirTextoOculta(puntuacion.ToString("000000"), 145, 60,
          0xFF, 0xFF, 0x88, fuenteSans);
        Hardware.EscribirTextoOculta("Mejor punt.", 260, 60,
          0x88, 0xFF, 0xFF, fuenteSans);
        Hardware.EscribirTextoOculta(mejorPunt.ToString("000000"), 385, 60,
          0xFF, 0xFF, 0x88, fuenteSans);
        Hardware.EscribirTextoOculta("Nivel", 500, 60,
          0x88, 0xFF, 0xFF, fuenteSans);
        Hardware.EscribirTextoOculta(miJuego.GetNumeroNivel().ToString(), 565, 60,
          0xFF, 0xFF, 0x88, fuenteSans);
        Hardware.EscribirTextoOculta("Vidas", 615, 60,
          0x88, 0xFF, 0xFF, fuenteSans);
        for (byte i = 0; i < miJuego.GetPersonaje().GetNumVidas() - 1; i++)
            iconoVida.Dibujar((short)(672 + i * 60), 35);
    }

}
