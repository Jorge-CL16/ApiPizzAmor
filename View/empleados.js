const apiPuestoUrl = "https://localhost:44370/api/Puesto";
const apiEmpleadoUrl = "https://localhost:44370/api/Empleado";

let empleados = [];
let puestos = [];

/* ============================
   Funciones para Puestos
============================ */

// Cargar la tabla de Puestos
async function cargarPuestosEmpleados() {
  try {
    const response = await fetch(apiPuestoUrl);
    const data = await response.json();
    const tabla = document.getElementById("tablaPuestos");
    tabla.innerHTML = "";
    // Se actualiza el arreglo global de puestos (útil para la carga del select)
    puestos = data.data;
    data.data.forEach(puesto => {
      tabla.innerHTML += `
        <tr>
          <td>${puesto.idPuesto}</td>
          <td>${puesto.nombre}</td>
          <td>
            <button class="btn btn-warning btn-sm" onclick='editarPuesto(${JSON.stringify(puesto)})'>Editar</button>
            <button class="btn btn-danger btn-sm" onclick="eliminarPuesto(${puesto.idPuesto})">Eliminar</button>
          </td>
        </tr>
      `;
    });
  } catch (error) {
    console.error("Error al cargar puestos:",error);
  }
}

// Mostrar el modal para agregar un nuevo Puesto
function mostrarModalPuesto() {
  document.getElementById("idPuesto").value = "";
  document.getElementById("nombrePuesto").value = "";
  new bootstrap.Modal(document.getElementById("modalPuesto")).show();
}

// Cargar en el formulario del modal los datos del puesto a editar
function editarPuesto(puesto) {
  document.getElementById("idPuesto").value = puesto.idPuesto;
  document.getElementById("nombrePuesto").value = puesto.nombre;
  new bootstrap.Modal(document.getElementById("modalPuesto")).show();
}

// Guardar (agregar o actualizar) un Puesto
async function guardarPuesto() {
  const id = document.getElementById("idPuesto").value;
  const nombre = document.getElementById("nombrePuesto").value.trim();

  if (!nombre) {
    alert("El nombre del puesto es obligatorio.");
    return;
  }

  // Se crea el objeto con el formato que espera la API
  const puestoObj = {
    idPuesto: id ? parseInt(id) : 0,
    nombre: nombre
  };
  const method = id ? "PUT" : "POST";
  try {
    const response = await fetch(apiPuestoUrl, {
      method: method,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(puestoObj)
    });
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText);
    }
    // Se cierra el modal y se actualizan las tablas y selects
    bootstrap.Modal.getInstance(document.getElementById("modalPuesto")).hide();
    await cargarPuestosEmpleados();
    await cargarPuestosSelect();
  } catch (error) {
    console.error("Error al guardar el puesto:", error);
    alert("No se pudo guardar el puesto: " + error.message);
  }
}

// Eliminar un Puesto
async function eliminarPuesto(id) {
  if (confirm("¿Seguro que deseas eliminar este puesto?")) {
    try {
      const response = await fetch(`${apiPuestoUrl}/${id}`, { method: "DELETE" });
      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText);
      }
      await cargarPuestosEmpleados();
      await cargarPuestosSelect();
    } catch (error) {
      console.error("Error al eliminar el puesto:", error);
      alert("No se pudo eliminar el puesto: " + error.message);
    }
  }
}

// Cargar el select de puestos (para el formulario de empleados)
async function cargarPuestosSelect() {
  try {
    const respuesta = await fetch(apiPuestoUrl);
    const data = await respuesta.json();
    puestos = data.data; // actualiza el arreglo global
    const select = document.getElementById('puesto');
    select.innerHTML = '<option value="">Seleccione un puesto</option>';
    puestos.forEach(p => {
      select.innerHTML += `<option value="${p.idPuesto}">${p.nombre}</option>`;
    });
  } catch (error) {
    console.error("Error al cargar el select de puestos:", error);
  }
}

/* ============================
   Funciones para Empleados
============================ */

// Cargar la tabla de Empleados
async function cargarEmpleadosEmpleados() {
  try {
    const respuesta = await fetch(apiEmpleadoUrl);
    const data = await respuesta.json();
    empleados = data.data;
    const tbody = document.getElementById('tabla-empleados');
    tbody.innerHTML = '';
    empleados.forEach(emp => {
      // Buscar el nombre del puesto asociado al empleado
      const puestoNombre = puestos.find(p => p.idPuesto === emp.idPuesto)?.nombre || 'Sin puesto';
      tbody.innerHTML += `
        <tr>
          <td>${emp.idEmpleado}</td>
          <td>${emp.nombre}</td>
          <td>${emp.apellido}</td>
          <td>${emp.edad}</td>
          <td>${emp.sexo}</td>
          <td>${emp.fechaContratacion}</td>
          <td>${puestoNombre}</td>
          <td>
            <button class="btn btn-warning btn-sm" onclick='editarEmpleado(${emp.idEmpleado})'>Editar</button>
            <button class="btn btn-danger btn-sm" onclick='eliminarEmpleado(${emp.idEmpleado})'>Eliminar</button>
          </td>
        </tr>`;
    });
  } catch (error) {
    console.error("Error al cargar: ");
    alert("No se pudieron cargar los empleados.");
  }
}

// Mostrar el modal para agregar un nuevo Empleado
function mostrarFormularioAgregar() {
  document.getElementById('modalTitle').textContent = 'Agregar Empleado';
  document.getElementById('idEmpleado').value = '';
  document.getElementById('nombre').value = '';
  document.getElementById('apellido').value = '';
  document.getElementById('edad').value = '';
  document.getElementById('sexo').value = 'M';
  document.getElementById('fechaContratacion').value = '';
  document.getElementById('puesto').value = '';
  new bootstrap.Modal(document.getElementById('modalEmpleado')).show();
}

// Guardar (agregar o actualizar) un Empleado
async function guardarEmpleado() {
  const idEmpleado = document.getElementById('idEmpleado').value;
  const nombre = document.getElementById('nombre').value.trim();
  const apellido = document.getElementById('apellido').value.trim();
  const edad = document.getElementById('edad').value;
  const sexo = document.getElementById('sexo').value;
  const fechaContratacion = document.getElementById('fechaContratacion').value;
  const idPuesto = document.getElementById('puesto').value;

  console.log({ nombre, apellido, edad, fechaContratacion, idPuesto });

  if (!nombre || !apellido || !edad || !fechaContratacion) {
    alert("Todos los campos son obligatorios");
    return;
  }

  const empleado = {
    idEmpleado: idEmpleado ? parseInt(idEmpleado) : 0,
    nombre,
    apellido,
    edad: parseInt(edad),
    sexo,
    fechaContratacion,
    idPuesto: parseInt(idPuesto)
  };

  try {
    const metodo = idEmpleado ? 'PUT' : 'POST';
    const respuesta = await fetch(apiEmpleadoUrl, {
      method: metodo,
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(empleado)
    });

    const data = await respuesta.json();
    if (!respuesta.ok) {
      console.error("Error en la solicitud:", data);
      alert('Error: ${data.mensaje || "No se pudo guardar el empleado"}');
      return;
    }

    alert(data.mensaje || "Empleado guardado exitosamente");
    document.getElementById('modalEmpleado').querySelector('.btn-close').click();
    await cargarEmpleadosEmpleados();
  } catch (error) {
    console.error("Error en la solicitud:", error);
    alert("Hubo un error al procesar la solicitud.");
  }
}

// Cargar los datos de un Empleado en el formulario de edición y mostrar el modal
async function editarEmpleado(id) {
  const empleado = empleados.find(emp => emp.idEmpleado == id);
  if (!empleado) {
    console.error("Empleado no encontrado.");
    return;
  }

  document.getElementById('modalTitle').textContent = 'Editar Empleado';
  document.getElementById('idEmpleado').value = empleado.idEmpleado;
  document.getElementById('nombre').value = empleado.nombre;
  document.getElementById('apellido').value = empleado.apellido;
  document.getElementById('edad').value = empleado.edad;
  document.getElementById('sexo').value = empleado.sexo;
  document.getElementById('fechaContratacion').value = empleado.fechaContratacion;
  document.getElementById('puesto').value = empleado.idPuesto;

  new bootstrap.Modal(document.getElementById('modalEmpleado')).show();
}

// Eliminar un Empleado
async function eliminarEmpleado(id) {
  if (confirm('¿Seguro que deseas eliminar este empleado?')) {
    try {
      const respuesta = await fetch(`${apiEmpleadoUrl}/${id}`, { method: 'DELETE' });
      if (!respuesta.ok) {
        const errorData = await respuesta.json();
        console.error("Error en DELETE:", errorData);
        alert("Error al eliminar el empleado.");
        return;
      }
      await cargarEmpleadosEmpleados();
    } catch (error) {
      console.error("Error en la solicitud:", error);
      alert("Error al eliminar el empleado.");
    }
  }
}

/* ============================
   Inicialización
============================ */
// Cargar los datos al iniciar la página
cargarPuestosEmpleados();
cargarPuestosSelect().then(cargarEmpleadosEmpleados);