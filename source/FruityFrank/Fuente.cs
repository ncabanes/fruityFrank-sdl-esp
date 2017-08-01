/* --------------------------------------------------         
Fuente: tipos de letra para escribir en pantalla
Parte de Fruity Frank - Remake

@see Hardware Imagen
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.20  08-Dic-2008  Nacho Cabanes
                    Clase vacia, generado semi-autom.
0.21  09-Dic-2008  Eduardo y Javi
                    Creadas funciones Cargar, Escribir y LeerPuntero

--------------------------------------------------- */

using System;

public class Fuente
{
    // Atributos

    IntPtr punteroInterno;

    // Operaciones

    /// Constructor a partir de un nombre de fichero y un tamaño
    public Fuente(string nombreFichero, short tamanyo)
    {
        Cargar(nombreFichero, tamanyo);
    }

    public void Cargar(string nombreFichero, short tamanyo)
    {
        punteroInterno = Hardware.CargarFuente(nombreFichero, tamanyo);
        if (punteroInterno == IntPtr.Zero)
            Hardware.ErrorFatal("Fuente inexistente: " + nombreFichero);
    }

    /*public  void Escribir(short x, short y)
    {
      Hardware.EscribirTextoOculta(punteroInterno, x,y);
    }*/

    public IntPtr LeerPuntero()
    {
        return punteroInterno;
    }

}
