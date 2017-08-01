/* --------------------------------------------------         
Mapa2: mapa del segundo nivel de juego
Parte de Fruity Frank - Remake

@see Nivel Mapa
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, idéntica a la 0.24 de 2008
---------------------------------------------------- */

public class Mapa2 : Mapa
{

    public Mapa2()
    {
        mapaOriginal[0] = "YCYYMY YYYMCYYY";
        mapaOriginal[1] = "MYYCYY YYYMYCMM";
        mapaOriginal[2] = "YMCYYY YYYCPYYC";
        mapaOriginal[3] = "YYCYCY CYYYYYCY";
        mapaOriginal[4] = "      N        ";
        mapaOriginal[5] = "YYMCYM YYYCYYYY";
        mapaOriginal[6] = "YYYYYY YYYYYMYY";
        mapaOriginal[7] = "YYYPYC YYYYYYYY";
        mapaOriginal[8] = "YYPYYC YYYYYCYY";
        mapaOriginal[9] = "YYYYYY YYCYCYYC";

        Reiniciar();
    }
}
