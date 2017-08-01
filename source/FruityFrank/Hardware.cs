/* --------------------------------------------------         
Cereza: uno de los tipos de elementos graficos del juego   
Parte de Fruity Frank - Remake

@see ElemGrafico
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.20  08-Dic-2008  Nacho Cabanes
                    Clase básica: incluye Inicializar, DibujarImagenOculta,
                    VisualizarOculta, CargarImagen, TeclaPulsada,
                    RectanguloRellenoRGBA, RectanguloGBBA, Pausa, ErrorFatal,
                    GetAncho, GetAlto, definiciones de teclas
0.21  09-Dic-2008  Eduardo y Javi
                    Completado, CargarFuente, EscribirTextoOculta
                    Enmascarado puntero                      
0.25  06-Ene-2009  Nacho Cabanes
                    Añadida la posibilidad de reproducir sonidos:
                    el inicializador prepara SDL_mixer
0.26  18-Mar-2012  Nacho Cabanes
                    Añadida la posibilidad de cambiar a pantalla completa
0.27  01-May-2016  Nacho Cabanes
                    Corregido el bug que hacía perder memoria al escribir texto
---------------------------------------------------- */

using System;
using System.Threading;
using Tao.Sdl;

public class Hardware
{
    // Atributos

    static IntPtr pantallaOculta;
    static short ancho, alto;
    static int colores;
    static bool pantallaCompleta;
    static int flags;

    // Operaciones

    /// Inicializa el modo grafico a un cierto ancho, alto y profundidad de color, p.ej. 640, 480, 24 bits
    public static void Inicializar(short an, short al, int col, bool pantallaComp)
    {
        //System.Console.Write("Inicializando...");
        ancho = an;
        alto = al;
        colores = col;
        pantallaCompleta = pantallaComp;

        flags = (Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | Sdl.SDL_ANYFORMAT);
        if (pantallaCompleta)
            flags |= Sdl.SDL_FULLSCREEN;
        Sdl.SDL_Init(Sdl.SDL_INIT_EVERYTHING);
        pantallaOculta = Sdl.SDL_SetVideoMode(
          ancho,
          alto,
          colores,
          flags);

        Sdl.SDL_Rect rect2 =
          new Sdl.SDL_Rect(0, 0, (short)ancho, (short)alto);
        Sdl.SDL_SetClipRect(pantallaOculta, ref rect2);

        // Inicializamos tipos de letra TTF
        if (SdlTtf.TTF_Init() < 0)
            ErrorFatal("No se ha podido inicializar los tipos de letra TTF");
        // Inicializamos sonidos con SDL mixer
        if (SdlMixer.Mix_OpenAudio(22050,
            unchecked(Sdl.AUDIO_S16LSB), 2, 1024) == -1)
            ErrorFatal("No se ha podido inicializar el Sonido");

    }

    /// Dibuja una imagen en pantalla oculta, en ciertas coordenadas
    public static void BorrarPantallaOculta(byte r, byte g, byte b)
    {
        RectanguloRellenoRGBA(0, 0, ancho, alto, r, g, b, 0xFF);
    }

    /// Dibuja una imagen en pantalla oculta, en ciertas coordenadas
    public static void DibujarImagenOculta(IntPtr imagen, int x, int y)
    {
        Sdl.SDL_Rect origen = new Sdl.SDL_Rect(0, 0, ancho, alto);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect((short) x, (short) y, ancho, alto);
        Sdl.SDL_BlitSurface(imagen, ref origen, pantallaOculta, ref dest);
    }

    /// Dibuja una imagen en pantalla oculta, en ciertas coordenadas
    public static void DibujarImagenOculta(Imagen imagen, int x, int y)
    {
        DibujarImagenOculta(imagen.LeerPuntero(), x, y);
    }

    /// Visualiza la pantalla oculta
    public static void VisualizarOculta()
    {
        Sdl.SDL_Flip(pantallaOculta);
    }

    /// Carga una imagen (o sale con codigo de error si no existe)
    public static IntPtr CargarImagen(string fichero)
    {
        IntPtr imagen;
        imagen = SdlImage.IMG_Load(fichero);
        if (imagen == IntPtr.Zero)
        {
            ErrorFatal("Imagen inexistente: " + fichero);
        }
        return imagen;
    }

    /// Escribe un texto en la pantalla oculta
    /// (sobrecarga que usa la clase Fuente)
    public static void EscribirTextoOculta(string texto,
      short x, short y, byte r, byte g, byte b, Fuente f)
    {
        EscribirTextoOculta(texto, x, y, r, g, b, f.LeerPuntero());
    }

    /// Escribe un texto en la pantalla oculta
    /// (sobrecarga que contiene la lógica)
    static void EscribirTextoOculta(string texto,
      short x, short y, byte r, byte g, byte b, IntPtr fuente)
    {
        Sdl.SDL_Color color = new Sdl.SDL_Color(r, g, b);
        IntPtr textoComoImagen = SdlTtf.TTF_RenderText_Solid(
          fuente, texto, color);
        if (textoComoImagen == IntPtr.Zero)
            Environment.Exit(5);

        Sdl.SDL_Rect origen = new Sdl.SDL_Rect(0, 0, ancho, alto);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect(x, y, ancho, alto);

        Sdl.SDL_BlitSurface(textoComoImagen, ref origen,
          pantallaOculta, ref dest);
        Sdl.SDL_FreeSurface(textoComoImagen);
    }

    public static IntPtr CargarFuente(string fichero, short tamanyo)
    {
        IntPtr fuente = SdlTtf.TTF_OpenFont(fichero, tamanyo);
        if (fuente == IntPtr.Zero)
        {
            System.Console.WriteLine("Fuente inexistente: {0}", fichero);
            Environment.Exit(6);
        }
        return fuente;
    }

    public static bool TeclaPulsada(int c)
    {
        bool pulsada = false;
        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event suceso;
        Sdl.SDL_PollEvent(out suceso);
        /*if (suceso.type == Sdl.SDL_KEYDOWN)
          if (suceso.key.keysym.sym == c)
            pulsada = true;*/
        int numkeys;
        byte[] teclas = Tao.Sdl.Sdl.SDL_GetKeyState(out numkeys);
        if (teclas[c] == 1)
            pulsada = true;
        return pulsada;
    }

    public static void Pausa(int milisegundos)
    {
        Thread.Sleep(milisegundos);
    }

    public static void RectanguloRGBA(short x1, short y1, short x2, short y2,
      byte r, byte g, byte b, byte a)
    {
        SdlGfx.rectangleRGBA(pantallaOculta,
          x1, y1, x2, y2, r, g, b, a);
    }

    public static void RectanguloRellenoRGBA(short x1, short y1, short x2, short y2,
      byte r, byte g, byte b, byte a)
    {
        SdlGfx.boxRGBA(pantallaOculta,
          x1, y1, x2, y2, r, g, b, a);
    }

    /// Devuelve la anchura de la pantalla, en pixeles
    public static int GetAncho()
    {
        return ancho;
    }

    /// Devuelve la altura de la pantalla, en pixeles
    public static int GetAlto()
    {
        return alto;
    }

    public static void CambiarPantallaCompleta()
    {
        //Sdl.SDL_WM_ToggleFullScreen(pantallaOculta);

        // Forma alternativa, más fiable según
        // http://sdl.beuc.net/sdl.wiki/SDL_WM_ToggleFullScreen

        flags = flags ^ Sdl.SDL_FULLSCREEN;

        pantallaOculta = Sdl.SDL_SetVideoMode(
            ancho, alto, colores, flags);
    }

    /// Abandona el programa, mostrando un cierto mensaje de error
    public static void ErrorFatal(string texto)
    {
        System.Console.WriteLine(texto);
        System.Console.WriteLine("Pulse una tecla para terminar...");
        System.Console.ReadLine();
        Environment.Exit(1);
    }


    // Definiciones de teclas
    public static int TECLA_ESC = Sdl.SDLK_ESCAPE;
    public static int TECLA_ESP = Sdl.SDLK_SPACE;
    public static int TECLA_A = Sdl.SDLK_a;
    public static int TECLA_B = Sdl.SDLK_b;
    public static int TECLA_C = Sdl.SDLK_c;
    public static int TECLA_D = Sdl.SDLK_d;
    public static int TECLA_E = Sdl.SDLK_e;
    public static int TECLA_F = Sdl.SDLK_f;
    public static int TECLA_G = Sdl.SDLK_g;
    public static int TECLA_H = Sdl.SDLK_h;
    public static int TECLA_I = Sdl.SDLK_i;
    public static int TECLA_J = Sdl.SDLK_j;
    public static int TECLA_K = Sdl.SDLK_k;
    public static int TECLA_L = Sdl.SDLK_l;
    public static int TECLA_M = Sdl.SDLK_m;
    public static int TECLA_N = Sdl.SDLK_n;
    public static int TECLA_O = Sdl.SDLK_o;
    public static int TECLA_P = Sdl.SDLK_p;
    public static int TECLA_Q = Sdl.SDLK_q;
    public static int TECLA_R = Sdl.SDLK_r;
    public static int TECLA_S = Sdl.SDLK_s;
    public static int TECLA_T = Sdl.SDLK_t;
    public static int TECLA_U = Sdl.SDLK_u;
    public static int TECLA_V = Sdl.SDLK_v;
    public static int TECLA_W = Sdl.SDLK_w;
    public static int TECLA_X = Sdl.SDLK_x;
    public static int TECLA_Y = Sdl.SDLK_y;
    public static int TECLA_Z = Sdl.SDLK_z;
    public static int TECLA_1 = Sdl.SDLK_1;
    public static int TECLA_2 = Sdl.SDLK_2;
    public static int TECLA_3 = Sdl.SDLK_3;
    public static int TECLA_4 = Sdl.SDLK_4;
    public static int TECLA_5 = Sdl.SDLK_5;
    public static int TECLA_6 = Sdl.SDLK_6;
    public static int TECLA_7 = Sdl.SDLK_7;
    public static int TECLA_8 = Sdl.SDLK_8;
    public static int TECLA_9 = Sdl.SDLK_9;
    public static int TECLA_0 = Sdl.SDLK_0;
    public static int TECLA_ARR = Sdl.SDLK_UP;
    public static int TECLA_ABA = Sdl.SDLK_DOWN;
    public static int TECLA_DER = Sdl.SDLK_RIGHT;
    public static int TECLA_IZQ = Sdl.SDLK_LEFT;

    public static int TECLA_F2 = Sdl.SDLK_F2;

}
