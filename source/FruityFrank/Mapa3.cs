/* --------------------------------------------------         
Mapa3: mapa del tercer nivel de juego
Parte de Fruity Frank - Remake

@see Nivel Mapa
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, idéntica a la 0.24 de 2008
---------------------------------------------------- */

public class Mapa3 : Mapa
{
    public Mapa3()
    {
        mapaOriginal[0] = "ZCZZMZZZ ZPCZZM";
        mapaOriginal[1] = "MZZCZZPZ ZMZCMM";
        mapaOriginal[2] = "ZMCZZZZZ ZCPZZC";
        mapaOriginal[3] = "        N      ";
        mapaOriginal[4] = "ZZCZCZCZ ZZZZCZ";
        mapaOriginal[5] = "ZZMCZMZZ ZCZZZZ";
        mapaOriginal[6] = "ZZZZZZZZ ZZZMZZ";
        mapaOriginal[7] = "ZZZPZCMZ ZZZZZZ";
        mapaOriginal[8] = "ZZPZZCZZ ZZMCZZ";
        mapaOriginal[9] = "ZZZZZZZZ CZCZZC";

        Reiniciar();
    }
}