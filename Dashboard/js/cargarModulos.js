
//Agregue esto
function cargarModuloAlmacen() {
    fetch("modulos/almacen.html")
      .then((response) => {
        if (!response.ok) {
          throw new Error(`Error HTTP: ${response.status}`);
        }
        return response.text();
      })
      .then((html) => {
        document.getElementById("divContenedorPrincipal").innerHTML = html;
        console.log("📌 Almacén cargado correctamente.");
        cargarRefrescosAlmacen();
      })
      .catch((error) =>
        console.error("Error al cargar el módulo de almacén:", error)
      );
  }
  async function cargarModuloEmpleados() {
    try {
      console.log("Cargando módulo de empleados...");
      const url = "modulos/puestoYempleados.html";
      const resp = await fetch(url);
      if (!resp.ok) {
        throw new Error(`Error HTTP: ${resp.status}`);
      }
      const contenido = await resp.text();
      document.getElementById("divContenedorPrincipal").innerHTML = contenido;
      console.log("📌 Módulo de empleados cargado.");
      cargarEmpleadosEmpleados()
      cargarPuestosEmpleados()
    } catch (error) {
      console.error("Error al cargar el módulo de empleados:", error);
    }
  }
  
  async function cargarModuloOrden() {
    try {
      console.log("Cargando módulo de órdenes...");
      const url = "modulos/ordenes.html";
      const resp = await fetch(url);
      if (!resp.ok) {
        throw new Error(`Error HTTP: ${resp.status}`);
      }
      const contenido = await resp.text();
      document.getElementById("divContenedorPrincipal").innerHTML = contenido;
      console.log("📌 Módulo de órdenes cargado.");
      // Llamamos a la función de inicialización del módulo para asignar los event listeners y cargar datos.
      initializeOrdenModule();
    } catch (error) {
      console.error("Error al cargar el módulo de órdenes:", error);
    }
  }
  
  async function cargarModuloTipos() {
    try {
      console.log("Cargando módulo de Tipos de Pizza...");
      const url = "modulos/TiposdePizza.html";
      const resp = await fetch(url);
      if (!resp.ok) {
        throw new Error(`Error HTTP: ${resp.status}`);
      }
      const html = await resp.text();
      document.getElementById("divContenedorPrincipal").innerHTML = html;
      console.log("Módulo de Tipos de Pizza cargado correctamente.");
  
      // Se llama a la función de inicialización del módulo, si existe
      if (typeof window.initializePizzaModule === "function") {
        window.initializePizzaModule();
      } else {
        console.error("La función initializePizzaModule no está definida.");
      }
    } catch (error) {
      console.error("Error al cargar el módulo de Tipos de Pizza:", error);
    }
  }
  
  async function cargarModuloClientes() {
    try {
      console.log("Cargando módulo de clientes...");
      const url = "modulos/clientes.html";
      const resp = await fetch(url);
      if (!resp.ok) {
        throw new Error(`Error HTTP: ${resp.status}`);
      }
      const contenido = await resp.text();
      document.getElementById("divContenedorPrincipal").innerHTML = contenido;
      console.log("Módulo de clientes cargado.");
      cargarClientesClientes();
    } catch (error) {
      console.error("Error al cargar el módulo de clientes:", error);
    }
  }
  
  async function cargarModuloMenu() {
    try {
      console.log("Cargando módulo de menú...");
      const url = "modulos/menu.html";
      const resp = await fetch(url);
      if (!resp.ok) {
        throw new Error(`Error HTTP: ${resp.status}`);
      }
      const html = await resp.text();
      document.getElementById("divContenedorPrincipal").innerHTML = html;
      console.log("Menú cargado correctamente.");
      
      // Una vez inyectado el HTML, inicializamos las funciones del módulo de menú
      loadSizesMenu();
      loadTypesMenu();
      loadMenusMenu();
    } catch (error) {
      console.error("Error al cargar el módulo de menú:", error);
    }
  }