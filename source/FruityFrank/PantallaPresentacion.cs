/* --------------------------------------------------         
PantallaPresentacion: pantalla de presentación
Parte de Fruity Frank - Remake

@see Hardware Juego Imagen
@author  Nacho

Versiones:

Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, casi idéntica a la 0.21 de 2008
0.31  25-Ago-2017  Renombrada de "Presentacion" a "PantallaPresentacion"
                   Se puede cargar las pantallas de Ayuda, Records, Créditos
---------------------------------------------------- */

public class PantallaPresentacion
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
    public PantallaPresentacion()
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

        string[] textos = 
        { "Pulsa: ",
            " - ESPACIO para empezar",
            " - A para Ayuda",
            " - R para ver los Records",
            " - C para Créditos",
            " - S para Salir"
        };

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
            for (int i = 0; i < textos.Length; i++)
            {
                Hardware.EscribirTextoOculta(
                    textos[i],
                    410, (short) (400 + i*40), 
                    (byte) (255-i*30), (byte)(255 - i * 30), 0x00, 
                    fuenteSans);
            }
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

            if (Hardware.TeclaPulsada(Hardware.TECLA_A))
            {
                PantallaAyuda miAyuda = new PantallaAyuda();
                miAyuda.Ejecutar();
            }

            if (Hardware.TeclaPulsada(Hardware.TECLA_C))
            {
                PantallaCreditos creditos = new PantallaCreditos();
                creditos.Ejecutar();
            }

            if (Hardware.TeclaPulsada(Hardware.TECLA_R))
            {
                PantallaRecords records = new PantallaRecords();
                records.Ejecutar();
            }

            // Pausa de 20 ms, para velocidad de 50 fps (1000/20 = 50)
            Hardware.Pausa(20);
        }
    }
}
