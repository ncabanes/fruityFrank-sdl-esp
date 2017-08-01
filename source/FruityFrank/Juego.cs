/* --------------------------------------------------         
Juego: parte principal del juego   
Parte de Fruity Frank - Remake

@see Fruity
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, casi idéntica a la 0.26 de 2009
                   Excepto: el personaje se mueve a la posición de inicio del nivel
                   y desglosado "Main" a la clase Fruity
---------------------------------------------------- */

public class Juego
{
    // Atributos
    private Personaje miPersonaje;
    private Presentacion miPresentacion;
    private Nivel miNivel;
    private Marcador miMarcador;

    private int numeroNivel;
    private Fuente fuenteSans14;

    private Sonido musicaNuevoNivel;

    private bool partidaTerminada = false;  // Si termina una partida
    private bool juegoTerminado = false;  // Si termina toda la sesión de juego


    //public Disparo  myDisparo;
    //public TablaRecords  myTablaRecords;


    // Operaciones

    public Juego()  // Constructor
    {
        bool pantallaCompleta = false;
        Hardware.Inicializar(1024, 768, 24, pantallaCompleta);

        miPresentacion = new Presentacion();
        miPersonaje = new Personaje(this);
        miNivel = new Nivel1();
        miPersonaje.MoverA(miNivel.GetXSalida(), miNivel.GetYSalida());
        miMarcador = new Marcador(this);

        fuenteSans14 = new Fuente("FreeSansBold.ttf", 14);
        musicaNuevoNivel = new Sonido("sonidos\\fruity-cambioNivel.mp3");

        numeroNivel = 1;
    }

    /// Bucle principal del juego
    public void BuclePrincipal()
    {
        // Parte repetitiva ("bucle de juego")
        NuevaPartida();
        do
        {
            DibujarPantalla();
            ComprobarTeclas();
            ComprobarColisiones();
            SiguienteFotograma();
            // Pausa de 20 ms, para velocidad de 50 fps (1000/20 = 50)
            Hardware.Pausa(20);
            // Fin de la parte repetitiva
        } while (!partidaTerminada); // Hasta tecla ESC
    }

    private void DibujarPantalla()
    {
        Hardware.BorrarPantallaOculta(0, 0, 0); // Borro en negro
                                                // ##TODO: Por hacer
                                                // Dibujar nivel
                                                // Dibujar personaje
        miMarcador.Dibujar();
        miNivel.Dibujar();
        miPersonaje.Dibujar();
        // Dibujar enemigo
        // Dibujar disparo (si lo hay)
        // Dibujar lluvia (si la hay)
        Hardware.VisualizarOculta();
    }

    /// Comprueba teclas, para ver si hay que mover personaje, disparar
    /// o salir
    private void ComprobarTeclas()
    {
        if (Hardware.TeclaPulsada(Hardware.TECLA_ABA))
            miPersonaje.MoverAbajo();

        if (Hardware.TeclaPulsada(Hardware.TECLA_ARR))
            miPersonaje.MoverArriba();

        if (Hardware.TeclaPulsada(Hardware.TECLA_DER))
            miPersonaje.MoverDerecha();

        if (Hardware.TeclaPulsada(Hardware.TECLA_IZQ))
            miPersonaje.MoverIzquierda();

        if (Hardware.TeclaPulsada(Hardware.TECLA_ESC))
            partidaTerminada = true;

        if (Hardware.TeclaPulsada(Hardware.TECLA_F2))
            Hardware.CambiarPantallaCompleta();
    }

    /// Comprueba colisiones del personaje con "enemigos"
    /// Las colisiones con "premios" las comprueba el personaje al intentar mover
    private void ComprobarColisiones()
    {
        foreach (Enemigo e in miNivel.GetEnemigos())
            if (e.ColisionCon(miPersonaje))
            {
                miNivel.PararMusica();
                miPersonaje.PerderVida();
                miNivel.ReproducirMusica();
                //disparoActivo = false;
                if (miPersonaje.GetNumVidas() == 0)
                    PartidaTerminada();
                break;  // Para no perder 2 vidas si se choca con 2
            }
    }

    /// Anima los enemigos y demás elementos del nivel.
    /// Cambia el nivel si corresponde.    
    private void SiguienteFotograma()
    {
        // Animo el personaje, el nivel y enemigos
        miPersonaje.Mover();
        miNivel.SiguienteFotograma();
        if (miNivel.GetCompleto())
            SiguienteNivel();
    }


    /// Anima los enemigos y demás elementos del nivel.
    /// Cambia el nivel si corresponde.    
    private void PartidaTerminada()
    {
        partidaTerminada = true;  // Fin de partida
        miNivel.PararMusica();
        miMarcador.CompruebaRecord();   // Y compruebo si es record
        Hardware.RectanguloRellenoRGBA(200, 290, 440, 330, 0xff, 0xff, 0xff, 0x88);
        Hardware.EscribirTextoOculta("Fin de la partida  :-(", 260, 300,
          0xFF, 0xFF, 0x88, fuenteSans14);  // Aviso
        Hardware.VisualizarOculta();
        Hardware.Pausa(3000);
    }

    /// Cambia al siguiente nivel de juego
    public void SiguienteNivel()
    {
        MensajeAviso(" Siguiente nivel... ", 2000);
        miNivel.PararMusica();
        musicaNuevoNivel.Reproducir1();

        miPersonaje.Reiniciar();
        numeroNivel++;

        // Por ahora solo hay tres niveles, así que alterno:
        if (numeroNivel % 3 == 1)
            miNivel = new Nivel1();
        else if (numeroNivel % 3 == 2)
            miNivel = new Nivel2();
        else
            miNivel = new Nivel3();
        miNivel.ReproducirMusica();
    }


    public void LanzarPresentacion()
    {
        miPresentacion.Ejecutar();
        SetFinDelJuego(miPresentacion.GetSalirDelJuego());
    }

    private void NuevaPartida()
    {
        // Al comienzo de cada partida: marco como "no terminada"
        partidaTerminada = false;
        // Regenero el mapa inicial
        miNivel = new Nivel1();
        numeroNivel = 1;
        // Reinicializo personaje y marcador
        miPersonaje.Reiniciar();
        miPersonaje.SetNumVidas(3);
        miMarcador.SetPuntuacion(0);
        miNivel.ReproducirMusica();
    }

    /// Cambia al siguiente nivel de juego
    public void MensajeAviso(string texto, int pausa)
    {
        DibujarPantalla();
        Hardware.RectanguloRellenoRGBA(200, 200, 440, 280, 0xff, 0xff, 0xff, 0x88);
        Hardware.EscribirTextoOculta(texto, 250, 230,
          0xFF, 0xFF, 0x88, fuenteSans14);  // Aviso
        Hardware.VisualizarOculta();
        Hardware.Pausa(pausa);
    }

    public bool EsFinDelJuego()
    {
        return juegoTerminado;
    }

    public void SetFinDelJuego(bool valor)
    {
        juegoTerminado = valor;
    }

    /// Para que los elementos del juego puedan acceder a los detalles
    /// del nivel actual
    public Nivel GetNivelActual()
    {
        return miNivel;
    }

    /// Para que los elementos del juego puedan acceder a los detalles
    /// del nivel actual
    public int GetNumeroNivel()
    {
        return numeroNivel;
    }

    /// Para que los elementos del juego puedan acceder a datos del personaje,
    /// como las vidas
    public Personaje GetPersonaje()
    {
        return miPersonaje;
    }

    /// Para que los elementos del juego puedan acceder al marcador
    public Marcador GetMarcador()
    {
        return miMarcador;
    }

}
