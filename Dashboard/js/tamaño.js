const apiUrl = 'https://localhost:44370/api/TamañoDePizza';
    let tamanosList = []; 

    async function obtenerTamanos() {
      try {
        const response = await fetch(apiUrl);
        if (!response.ok) throw new Error("Error en la respuesta de red");
        const result = await response.json();
        if (result.exito === 1) {
          tamanosList = result.data;
          renderizarTabla(result.data);
        } else {
          console.error("Error al obtener datos:", result.mensaje);
        }
      } catch (error) {
        console.error("Error al obtener tamaños de pizza:", error);
      }
    }

    function renderizarTabla(tamanos) {
      const tbody = document.getElementById('tabla-body');
      tbody.innerHTML = "";
      tamanos.forEach(tamano => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
          <td>${tamano.idTamano}</td>
          <td>${tamano.nombre}</td>
          <td>${parseFloat(tamano.precio).toFixed(2)}</td>
          <td>
            <button class="btn btn-warning btn-sm" onclick="editarTamano(${tamano.idTamano})">Editar</button>
            <button class="btn btn-danger btn-sm" onclick="eliminarTamano(${tamano.idTamano})">Eliminar</button>
          </td>
        `;
        tbody.appendChild(tr);
      });
    }

    document.getElementById('tamanoForm').addEventListener('submit', async function(event) {
      event.preventDefault();
      const idTamano = document.getElementById('idTamano').value;
      const nombre = document.getElementById('nombre').value;
      const precio = parseFloat(document.getElementById('precio').value);
      const payload = { nombre, precio };

      try {
        let response;
        if (idTamano === "" || idTamano === null) {
          response = await fetch(apiUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
          });
        } else {
          payload.idTamano = parseInt(idTamano);
          response = await fetch(apiUrl, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
          });
        }
        if (!response.ok) throw new Error("Error en la respuesta de red");
        const result = await response.json();
        if (result.exito === 1) {
          obtenerTamanos();
          resetForm();
        } else {
          console.error("Error:", result.mensaje);
        }
      } catch (error) {
        console.error("Error al guardar:", error);
      }
    });

    async function eliminarTamano(id) {
      if (!confirm("¿Está seguro de eliminar este registro?")) return;
      try {
        const response = await fetch(`${apiUrl}/${id}`, { method: 'DELETE' });
        if (!response.ok) throw new Error("Error en la respuesta de red");
        const result = await response.json();
        if (result.exito === 1) {
          obtenerTamanos();
        } else {
          console.error("Error al eliminar:", result.mensaje);
        }
      } catch (error) {
        console.error("Error al eliminar:", error);
      }
    }

    function editarTamano(id) {
      const tamano = tamanosList.find(t => t.idTamano === id);
      if (tamano) {
        document.getElementById('idTamano').value = tamano.idTamano;
        document.getElementById('nombre').value = tamano.nombre;
        document.getElementById('precio').value = tamano.precio;
        document.getElementById('form-title').textContent = "Editar Tamaño de Pizza";
        document.getElementById('btnSubmit').textContent = "Actualizar";
        document.getElementById('btnCancel').style.display = "inline-block";
      } else {
        console.error("Registro no encontrado");
      }
    }

    document.getElementById('btnCancel').addEventListener('click', resetForm);
    function resetForm() {
      document.getElementById('idTamano').value = "";
      document.getElementById('nombre').value = "";
      document.getElementById('precio').value = "";
      document.getElementById('form-title').textContent = "Agregar Tamaño de Pizza";
      document.getElementById('btnSubmit').textContent = "Guardar";
      document.getElementById('btnCancel').style.display = "none";
    }

    obtenerTamanos();