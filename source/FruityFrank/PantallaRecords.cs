/* --------------------------------------------------         
PantallaRecords: pantalla de records
Parte de Fruity Frank - Remake

@see Hardware Juego Imagen
@author  Nacho

Versiones:

Num.   Fecha       Por / Cambios
---------------------------------------------------
0.31  25-Ago-2017  Nacho Cabanes
                   Creada la clase, basada en la pantalla de presentación
---------------------------------------------------- */

public class PantallaRecords
{
    // Atributos

    private ElemGrafico cartel;
    private Fuente fuenteSans;

    short xCartel = 32, yCartel = 50;

    // Operaciones

    /// Constructor
    public PantallaRecords()
    {
        cartel = new ElemGrafico("imagenes/records.png");
        fuenteSans = new Fuente("FreeSansBold.ttf", 24);
    }

    /// Lanza la pantalla de créditos
    public void Ejecutar()
    {
        Hardware.BorrarPantallaOculta(0, 0, 0); // Borro en negro
        cartel.Dibujar(xCartel, yCartel);
        Hardware.EscribirTextoOculta(
            "Pulsa ESC para volver al menú...",
            550, 10,
            255, 255, 0,
            fuenteSans);
        Hardware.VisualizarOculta();

        while (!Hardware.TeclaPulsada(Hardware.TECLA_ESC))
        {
            // Pausa de 20 ms, para velocidad de 50 fps (1000/20 = 50)
            Hardware.Pausa(20);
        }
    }
}
