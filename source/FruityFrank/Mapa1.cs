/* --------------------------------------------------         
Mapa1: mapa del primer nivel de juego
Parte de Fruity Frank - Remake

@see Nivel Mapa
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, idéntica a la 0.24 de 2008
---------------------------------------------------- */

public class Mapa1 : Mapa
{

    public Mapa1()
    {
        mapaOriginal[0] = "XCXXMXXX XMCXXX";
        mapaOriginal[1] = "MXXCXXXX XMXCMM";
        mapaOriginal[2] = "XMCXXXXX XCXXXC";
        mapaOriginal[3] = "XXCXCXCX XXXXCX";
        mapaOriginal[4] = "        N      ";
        mapaOriginal[5] = "XXMCXMXX XCXXXX";
        mapaOriginal[6] = "XXXXXXXX XXXMXX";
        mapaOriginal[7] = "XXXXXCXX XXXXXX";
        mapaOriginal[8] = "XXXXXCXX XXXCXX";
        mapaOriginal[9] = "XXXXXXXX CXCXXC";

        Reiniciar();
    }
}
