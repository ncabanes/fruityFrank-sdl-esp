/* --------------------------------------------------         
PantallaCreditos: pantalla de créditos
Parte de Fruity Frank - Remake

@see Hardware Juego Imagen
@author  Nacho

Versiones:

Num.   Fecha       Por / Cambios
---------------------------------------------------
0.31  25-Ago-2017  Nacho Cabanes
                   Creada la clase, basada en la pantalla de presentación
---------------------------------------------------- */

public class PantallaCreditos
{
    // Atributos

    private ElemGrafico cartel;
    private Fuente fuenteSans;

    short xCartel = 32, yCartel = 50;
    bool salir;


    // Operaciones

    /// Constructor
    public PantallaCreditos()
    {
        cartel = new ElemGrafico("imagenes/creditos.png");
        fuenteSans = new Fuente("FreeSansBold.ttf", 24);
    }

    /// Lanza la pantalla de créditos
    public void Ejecutar()
    {
        salir = false;

        while (!salir)
        {
            if (Hardware.TeclaPulsada(Hardware.TECLA_ESC))
            {
                salir = true;
            }
            Hardware.BorrarPantallaOculta(0, 0, 0); // Borro en negro
            cartel.Dibujar(xCartel, yCartel);

            Hardware.EscribirTextoOculta(
                "Remake (parcial), Nacho Cabanes 2008 & 2017...",
                189, 250,
                255, 128, 0,
                fuenteSans);
            Hardware.VisualizarOculta();

            Hardware.EscribirTextoOculta(
                "Pulsa ESC para volver al menú...",
                280, 550,
                255, 255, 0,
                fuenteSans);
            Hardware.VisualizarOculta();

            // Pausa de 20 ms, para velocidad de 50 fps (1000/20 = 50)
            Hardware.Pausa(20);
        }
    }
}
