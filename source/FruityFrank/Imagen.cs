/* --------------------------------------------------         
Imagen: oculta el manejo de imágenes con SDL   
Parte de Fruity Frank - Remake

@see ElemGrafico
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.20  08-Dic-2008  Nacho Cabanes
                    Clase básica: permite cargar y dibujar
0.22  14-Dic-2008  Nacho Cabanes
                    Añadidos constructor vacío y const.con nombre de fichero 
---------------------------------------------------- */

using System;

public class Imagen
{
    // Atributos

    IntPtr punteroInterno;

    // Operaciones
    public Imagen()  // Constructor
    {
        punteroInterno = IntPtr.Zero;  // En principio, no hay imagen
    }

    public Imagen(string nombreFichero)  // Constructor
    {
        Cargar(nombreFichero);
    }

    public void Cargar(string nombreFichero)
    {
        punteroInterno = Hardware.CargarImagen(nombreFichero);
        if (punteroInterno == IntPtr.Zero)
            Hardware.ErrorFatal("Imagen inexistente: " + nombreFichero);
    }

    public void Dibujar(int x, int y)
    {
        Hardware.DibujarImagenOculta(punteroInterno, x, y);
    }

    public IntPtr LeerPuntero()
    {
        return punteroInterno;
    }
}
