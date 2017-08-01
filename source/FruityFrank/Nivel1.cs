/* --------------------------------------------------         
Nivel1: primer nivel de juego
Parte de Fruity Frank - Remake

@see Nivel Mapa
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, idéntica a la 0.28
---------------------------------------------------- */

public class Nivel1 : Nivel
{
    const byte NUMENEMIGOS = 3;

    public Nivel1()
    {
        miMapa = new Mapa1();
        miMusicaFondo = new Sonido("sonidos\\fruity-nivel1.mp3");
        enemigos = new Enemigo[NUMENEMIGOS];

        enemigos[0] = new Nariz(this);
        enemigos[0].MoverA(miMapa.GetXNido(), miMapa.GetYNido());
        enemigos[0].SetRetardo(50);  // 1 segundo despues del comienzo

        enemigos[1] = new Nariz(this);
        enemigos[1].MoverA(miMapa.GetXNido(), miMapa.GetYNido());
        enemigos[1].SetRetardo(3*50);  // 3 segundos despues del comienzo

        enemigos[2] = new Pepino(this);
        enemigos[2].MoverA(
        (short)(miMapa.GetXIni() + 5 * miMapa.GetAnchoCasilla()),
        miMapa.GetYIni());
        enemigos[2].SetRetardo(150);  // 6 segundos despues del comienzo    
        enemigos[2].SetVelocidad(0, 4);

        PrepararManzanas();
    }
}
