/* --------------------------------------------------         
Enemigo: comportamiento común a varios enemigos (retardo)
Parte de Fruity Frank - Remake

@see ElemGrafico Pepino Nariz
@author  Nacho

Versiones:
   
Num.   Fecha       Por / Cambios
---------------------------------------------------
0.30  01-Ago-2017  Nacho Cabanes
                   Primera versión 2017, idéntica a la 0.24 de 2008
---------------------------------------------------- */

public class Enemigo : ElemGrafico
{
    protected Nivel miNivel;

    protected int retardo;                  // Para que aparezcan algo más tarde
    protected int contadorHastaRetardo;

    public Enemigo()  // Anchura, altura, etc
    {
        incrX = 3;
        framesPorFotograma = 4;
    }

    /// Da valor al retardo inicial (lo que tarda en aparecer)
    public void SetRetardo(int n)
    {
        retardo = n;
        contadorHastaRetardo = 0;
    }
}
