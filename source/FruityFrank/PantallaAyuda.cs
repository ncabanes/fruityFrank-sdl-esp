/* --------------------------------------------------         
PantallaAyuda: pantalla de ayuda
Parte de Fruity Frank - Remake

@see Hardware Juego Imagen
@author  Nacho

Versiones:

Num.   Fecha       Por / Cambios
---------------------------------------------------
0.31  25-Ago-2017  Nacho Cabanes
                   Creada la clase, basada en la pantalla de presentación
---------------------------------------------------- */

public class PantallaAyuda
{
    // Atributos

    private ElemGrafico cartel1, cartel2;
    private Fuente fuenteSans;

    short xCartel = 32, yCartel = 50;
    bool salir;


    // Operaciones

    /// Constructor
    public PantallaAyuda()
    {
        cartel1 = new ElemGrafico("imagenes/ayuda1.png");
        cartel2 = new ElemGrafico("imagenes/ayuda2.png");
        fuenteSans = new Fuente("FreeSansBold.ttf", 24);
    }

    /// Lanza la pantalla de ayuda
    public void Ejecutar()
    {
        int cartelActual = 0;
        int cantidadCarteles = 2;
        int contadorFotogramas = 0;
        int fotogramasHastaOtraImagen = 100;
        salir = false;

        while (!salir)
        {
            if (Hardware.TeclaPulsada(Hardware.TECLA_ESC))
            {
                salir = true;
            }
            Hardware.BorrarPantallaOculta(0, 0, 0); // Borro en negro
            if (cartelActual == 0)
                cartel1.Dibujar(xCartel, yCartel);
            else
                cartel2.Dibujar(xCartel, yCartel);
            Hardware.EscribirTextoOculta(
                "Pulsa ESC para volver al menú...",
                10, 10,
                (byte)(255 - contadorFotogramas*2), (byte)(255 - contadorFotogramas*2), 0,
                fuenteSans);
            Hardware.VisualizarOculta();

            contadorFotogramas++;

            if (contadorFotogramas >= fotogramasHastaOtraImagen)
            {
                contadorFotogramas = 0;
                cartelActual++;
                if (cartelActual >= cantidadCarteles)
                    cartelActual = 0;
            }

            // Pausa de 20 ms, para velocidad de 50 fps (1000/20 = 50)
            Hardware.Pausa(20);
        }
    }
}
