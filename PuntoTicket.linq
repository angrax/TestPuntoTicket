<Query Kind="Program" />

void Main()
{
	const string nodo = "4,3,16,10,7,20,12";
	var nodos = nodo.Split(',').Select(x => Convert.ToInt32(x)).ToList();

	var arbol = new ArbolBinario();
	for (var i = 0; i < nodos.Count; i++)
	{
		arbol.Agregar(nodos[i]);
	}

//	arbol.Dump();

	arbol.Distancia(arbol.Raiz, 7).Dump("a. la raíz y 7 la respuesta debería ser 3");
	arbol.Distancia(arbol.Raiz, 3).Dump("b. la raíz y 3 la respuesta debería ser 1");
	arbol.Distancia(arbol.Raiz, 40).Dump("c. la raíz y 40 la respuesta debería ser -1 (pues no existe)");

	arbol.Distancia(arbol.Raiz, 3, 12).Dump("a. entre 3 y 12 la respuesta es 4");
	arbol.Distancia(arbol.Raiz, 4, 4).Dump("entre 4 y 4 es 0");
	arbol.Distancia(arbol.Raiz, 3, 10).Dump("entre 10 y 3 la respuesta es 3");
	arbol.Distancia(arbol.Raiz, 4, 40).Dump("entre 4 y 40 es -1 (pues no existe)");
}

public class ArbolBinario
{
	public Nodo Raiz { get; set; }

	public void Agregar(int valor)
	{
		var nodo = new Nodo();
		nodo.Numero = valor;

		if (Raiz == null)
		{
			Raiz = nodo;
		}
		else
		{
			var nodoActual = Raiz;
			Nodo padre;

			while (true)
			{
				padre = nodoActual;
				if (valor < nodoActual.Numero)
				{
					nodoActual = nodoActual.Izquierda;
					if (nodoActual == null)
					{
						padre.Izquierda = nodo;
						return;
					}
				}
				else
				{
					nodoActual = nodoActual.Derecha;
					if (nodoActual == null)
					{
						padre.Derecha = nodo;
						return;
					}
				}
			}
		}
	}

	public int Distancia(Nodo nodo, int valor)
	{
		if (nodo == null)
		{
			return -1;
		}

		var retorno = -1;
		if ((nodo.Numero == valor) ||
			(retorno = Distancia(nodo.Izquierda, valor)) >= 0 ||
			(retorno = Distancia(nodo.Derecha, valor)) >= 0)
		{
			retorno += 1;
		}

		return retorno;
	}

	public int Distancia(Nodo nodo, int nodo1, int nodo2)
	{
		if (nodo == null)
		{
			return 0;
		}

		if (nodo.Numero > nodo1 && nodo.Numero > nodo2)
		{
			return Distancia(nodo.Izquierda, nodo1, nodo2);
		}

		if (nodo.Numero < nodo1 && nodo.Numero < nodo2)
		{
			return Distancia(nodo.Derecha, nodo1, nodo2);
		}

		if (nodo.Numero >= nodo1 && nodo.Numero <= nodo2)
		{
			return Distancia(nodo, nodo1) + Distancia(nodo, nodo2);
		}

		return 0;
	}
}

public class Nodo
{
	public Nodo Izquierda { get; set; }
	public Nodo Derecha { get; set; }
	public int Numero { get; set; }
}