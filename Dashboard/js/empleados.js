const apiPuestoUrl = "https://localhost:44370/api/Puesto";
    const apiEmpleadoUrl = "https://localhost:44370/api/Empleado";

    async function cargarPuestosEmpleados() {
      const response = await fetch(apiPuestoUrl);
      const data = await response.json();
      const tabla = document.getElementById("tablaPuestos");
      tabla.innerHTML = "";
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
    }

    function mostrarModalPuesto() {
      document.getElementById("idPuesto").value = "";
      document.getElementById("nombrePuesto").value = "";
      new bootstrap.Modal(document.getElementById("modalPuesto")).show();
    }

    function editarPuesto(puesto) {
      document.getElementById("idPuesto").value = puesto.idPuesto;
      document.getElementById("nombrePuesto").value = puesto.nombre;
      new bootstrap.Modal(document.getElementById("modalPuesto")).show();
    }

    async function guardarPuesto() {
      const id = document.getElementById("idPuesto").value;
      const nombre = document.getElementById("nombrePuesto").value;
      const puesto = { idPuesto: id ? parseInt(id) : 0, puesto: nombre };
      const method = id ? "PUT" : "POST";

      await fetch(apiPuestoUrl, {
        method: method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(puesto)
      });
      bootstrap.Modal.getInstance(document.getElementById("modalPuesto")).hide();
      cargarPuestosEmpleados();
      cargarPuestosSelect();
    }

    async function eliminarPuesto(id) {
      if (confirm("¿Seguro que deseas eliminar este puesto?")) {
        await fetch(`${apiPuestoUrl}/${id}`, { method: "DELETE" });
        cargarPuestosEmpleados();
        cargarPuestosSelect();
      }
    }

    let empleados = [];
    let puestos = [];

    
    async function cargarPuestosSelect() {
      const respuesta = await fetch(apiPuestoUrl);
      const data = await respuesta.json();
      puestos = data.data;
      const select = document.getElementById('puesto');
      select.innerHTML = '<option value="">Seleccione un puesto</option>';
      puestos.forEach(p => {
        select.innerHTML += `<option value="${p.idPuesto}">${p.nombre}</option>`;
      });
    }

    async function cargarEmpleadosEmpleados() {
      const respuesta = await fetch(apiEmpleadoUrl);
      const data = await respuesta.json();
      empleados = data.data;
      const tbody = document.getElementById('tabla-empleados');
      tbody.innerHTML = '';
      empleados.forEach(emp => {
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
    }

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

    async function guardarEmpleado() {
      const idEmpleado = document.getElementById('idEmpleado').value;
      const nombre = document.getElementById('nombre').value;
      const apellido = document.getElementById('apellido').value;
      const edad = document.getElementById('edad').value;
      const sexo = document.getElementById('sexo').value;
      const fechaContratacion = document.getElementById('fechaContratacion').value;
      const idPuesto = document.getElementById('puesto').value;

      if (!nombre || !apellido || !edad || !fechaContratacion || !idPuesto) {
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
          alert(`Error: ${data.mensaje || "No se pudo guardar el empleado"}`);
          return;
        }

        alert(data.mensaje || "Empleado guardado exitosamente");

        document.getElementById('modalEmpleado').querySelector('.btn-close').click();
        cargarEmpleadosEmpleados();
      } catch (error) {
        console.error("Error en la solicitud:", error);
        alert("Hubo un error al procesar la solicitud.");
      }
    }

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
          cargarEmpleadosEmpleados();
        } catch (error) {
          console.error("Error en la solicitud:", error);
        }
      }
    }

    cargarPuestosEmpleados();
    cargarPuestosSelect().then(cargarEmpleadosEmpleados);