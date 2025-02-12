const apiUrl = "https://localhost:44370/api/Refrescos";

document.addEventListener("DOMContentLoaded", cargarRefrescosAlmacen);

async function cargarRefrescosAlmacen() {
  try {
    const response = await fetch(apiUrl);
    if (!response.ok) {
      throw new Error(`Error HTTP: ${response.status}`);
    }

    const result = await response.json();
    console.log("Respuesta de la API:", result);

    const refrescos = result.data || result;
    const tbody = document.getElementById("tablaRefrescos");

    if (!tbody) {
      console.error("No se encontró el elemento con id 'tablaRefrescos'");
      return;
    }

    tbody.innerHTML = "";
    refrescos.forEach(refresco => {
      tbody.innerHTML += `
        <tr>
          <td>${refresco.idRefresco}</td>
          <td>${refresco.nombre}</td>
          <td>${refresco.precio}</td>
          <td>${refresco.tamano}</td>
          <td>
            <button class="btn btn-warning btn-sm" onclick='editarRefresco(${JSON.stringify(refresco)})'>Editar</button>
            <button class="btn btn-danger btn-sm" onclick="eliminarRefresco(${refresco.idRefresco})">Eliminar</button>
          </td>
        </tr>
      `;
    });
  } catch (error) {
    console.error("Error al cargar refrescos:", error);
  }
}

function mostrarModal() {
  document.getElementById("modalRefrescoLabel").textContent = "Agregar Refresco";
  document.getElementById("idRefresco").value = "";
  document.getElementById("nombre").value = "";
  document.getElementById("precio").value = "";
  document.getElementById("tamano").value = "";
  new bootstrap.Modal(document.getElementById("modalRefresco")).show();
}

async function guardarRefresco() {
  const id = document.getElementById("idRefresco").value;
  const nombre = document.getElementById("nombre").value;
  const precio = document.getElementById("precio").value;
  const tamano = document.getElementById("tamano").value;

  if (!nombre || !precio || !tamano) {
    alert("Todos los campos son obligatorios");
    return;
  }

  const refresco = {
    IdRefresco: id ? parseInt(id) : 0,
    Nombre: nombre,
    Precio: parseFloat(precio),
    Tamano: tamano
  };

  try {
    const method = id ? "PUT" : "POST";
    const response = await fetch(apiUrl, {
      method: method,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(refresco)
    });

    const result = await response.json();
    alert(result.mensaje || "Operación exitosa");
    
    bootstrap.Modal.getInstance(document.getElementById("modalRefresco")).hide();
    cargarRefrescosAlmacen();
  } catch (error) {
    console.error("Error al guardar refresco:", error);
  }
}

function editarRefresco(refresco) {
  document.getElementById("modalRefrescoLabel").textContent = "Editar Refresco";
  document.getElementById("idRefresco").value = refresco.idRefresco;
  document.getElementById("nombre").value = refresco.nombre;
  document.getElementById("precio").value = refresco.precio;
  document.getElementById("tamano").value = refresco.tamano;
  new bootstrap.Modal(document.getElementById("modalRefresco")).show();
}

async function eliminarRefresco(id) {
  if (confirm("¿Estás seguro de eliminar este refresco?")) {
    try {
      const response = await fetch(`${apiUrl}/${id}`, { method: "DELETE" });

      const result = await response.json();
      alert(result.mensaje || "Refresco eliminado");
      cargarRefrescosAlmacen();
    } catch (error) {
      console.error("Error al eliminar refresco:", error);
    }
  }
}
