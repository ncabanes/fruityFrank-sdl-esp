/* --------------------------------------------------         
Presentacion: pantalla de presentación
Parte de Fruity Frank - Remake

@see Hardware Juego Imagen
@author  Nacho

Versiones:

Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, casi idéntica a la 0.21 de 2008
---------------------------------------------------- */

public class Presentacion
{
    // Atributos

    private ElemGrafico cartel;
    private Fuente fuenteSans;

    short xCartel = 200, yCartel = 200;
    short anchoCartel = 690, altoCartel = 90;
    short incrXCartel = 2, incrYCartel = 2;
    bool salirPresentacion;
    bool salirDelJuego;


    // Operaciones

    /// Constructor
    public Presentacion()
    {
        cartel = new ElemGrafico("imagenes/cartel.png");
        fuenteSans = new Fuente("FreeSansBold.ttf", 24);
    }

    /// Devuelve "true" si el usuario ha elegido entrar al juego
    public bool GetSalirDelJuego()
    {
        return salirDelJuego;
    }

    /// Lanza la presentacion
    public void Ejecutar()
    {
        salirPresentacion = false;
        salirDelJuego = false;
        while (!salirPresentacion)
        {
            if (Hardware.TeclaPulsada(Hardware.TECLA_ESP))
            {
                salirPresentacion = true;
                salirDelJuego = false;
            }
            if (Hardware.TeclaPulsada(Hardware.TECLA_S))
            {
                salirPresentacion = true;
                salirDelJuego = true;
            }
            Hardware.BorrarPantallaOculta(0, 0, 0); // Borro en negro
            cartel.Dibujar(xCartel, yCartel);
            Hardware.EscribirTextoOculta(
              "Pulsa ESPACIO para empezar o S para salir",
              210, 440, 0xFF, 0xAA, 0xAA, fuenteSans);
            Hardware.VisualizarOculta();

            xCartel += incrXCartel;
            yCartel += incrYCartel;

            if (xCartel < 0)
            {
                xCartel = 0;
                incrXCartel = (short)-incrXCartel;
            }
            if (xCartel > Hardware.GetAncho() - anchoCartel)
            {
                xCartel = (short)(Hardware.GetAncho() - anchoCartel);
                incrXCartel = (short)-incrXCartel;
            }
            if (yCartel < 10)
            {
                incrYCartel = (short)-incrYCartel;
            }
            if (yCartel > Hardware.GetAlto() / 2 - altoCartel)
            {
                incrYCartel = (short)-incrYCartel;
            }

            // Pausa de 20 ms, para velocidad de 50 fps (1000/20 = 50)
            Hardware.Pausa(20);
        }
    }
}