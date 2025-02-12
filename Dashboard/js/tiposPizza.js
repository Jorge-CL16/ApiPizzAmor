(function() {
    const apiUrl = 'https://localhost:44370/api/TiposDePizza';
  
    function initializePizzaModule() {
      const pizzaModalEl = document.getElementById('pizzaModal');
      if (!pizzaModalEl) {
        console.error("Elemento 'pizzaModal' no encontrado.");
        return;
      }
      window.pizzaModal = new bootstrap.Modal(pizzaModalEl);
  
      const btnSave = document.getElementById('btnSave');
      if (btnSave) {
        btnSave.addEventListener('click', savePizza);
      } else {
        console.error("Elemento 'btnSave' no encontrado.");
      }
  
      pizzaModalEl.addEventListener('hidden.bs.modal', () => {
        const pizzaForm = document.getElementById('pizzaForm');
        if (pizzaForm) pizzaForm.reset();
        const idInput = document.getElementById('idTipoPizza');
        if (idInput) idInput.value = '';
        const pizzaModalLabel = document.getElementById('pizzaModalLabel');
        if (pizzaModalLabel) pizzaModalLabel.textContent = 'Agregar Tipo de Pizza';
      });
  
      loadPizzas();
    }
  
    async function loadPizzas() {
      try {
        const response = await fetch(apiUrl);
        const data = await response.json();
  
        console.log("Respuesta GET:", data);
  
        if (data.exito === 1) {
          const pizzas = data.data;
          let rows = '';
          pizzas.forEach(pizza => {
            rows += `
              <tr>
                <td>${pizza.idTipoPizza}</td>
                <td>${pizza.nombre}</td>
                <td>
                  <button class="btn btn-sm btn-info me-2" onclick="editPizza(${pizza.idTipoPizza}, '${pizza.nombre}')">Editar</button>
                  <button class="btn btn-sm btn-danger" onclick="deletePizza(${pizza.idTipoPizza})">Eliminar</button>
                </td>
              </tr>
            `;
          });
          const tableBody = document.getElementById('pizzaTableBody');
          if (tableBody) {
            tableBody.innerHTML = rows;
          } else {
            console.error("Elemento 'pizzaTableBody' no encontrado.");
          }
        } else {
          console.error("Error al obtener datos:", data.mensaje);
          alert('Error al cargar los datos: ' + data.mensaje);
        }
      } catch (error) {
        console.error('Error en GET:', error);
        alert('Ocurrió un error al obtener los datos. Revisa la consola para más detalles.');
      }
    }
  
    function savePizza() {
      const idInput = document.getElementById('idTipoPizza');
      const nombreInput = document.getElementById('nombre');
      if (!nombreInput) {
        alert("Elemento 'nombre' no encontrado.");
        return;
      }
      const id = idInput ? idInput.value : '';
      const nombre = nombreInput.value.trim();
  
      if (!nombre) {
        alert('El nombre es obligatorio.');
        return;
      }
  
      let payload = { nombre: nombre };
      let method = 'POST';
      if (id) {
        method = 'PUT';
        payload.idTipoPizza = parseInt(id);
      }
  
      console.log("Enviando datos:", payload, "con método", method);
  
      fetch(apiUrl, {
        method: method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      })
        .then(response => response.json())
        .then(data => {
          console.log("Respuesta de guardar:", data);
          if (data.exito === 1) {
            if (window.pizzaModal) window.pizzaModal.hide();
            const pizzaForm = document.getElementById('pizzaForm');
            if (pizzaForm) pizzaForm.reset();
            loadPizzas();
          } else {
            alert('Error al guardar: ' + data.mensaje);
          }
        })
        .catch(error => {
          console.error('Error en POST/PUT:', error);
          alert('Ocurrió un error al guardar el registro. Revisa la consola para más detalles.');
        });
    }
  
    function editPizza(id, nombre) {
      const idInput = document.getElementById('idTipoPizza');
      const nombreInput = document.getElementById('nombre');
      if (!idInput || !nombreInput) {
        console.error("No se encontraron elementos del formulario.");
        return;
      }
      idInput.value = id;
      nombreInput.value = nombre;
      const pizzaModalLabel = document.getElementById('pizzaModalLabel');
      if (pizzaModalLabel) pizzaModalLabel.textContent = 'Editar Tipo de Pizza';
      if (window.pizzaModal) window.pizzaModal.show();
    }
  
    function deletePizza(id) {
      if (confirm('¿Estás seguro de eliminar este registro?')) {
        fetch(`${apiUrl}/${id}`, { method: 'DELETE' })
          .then(response => response.json())
          .then(data => {
            console.log("Respuesta de eliminar:", data);
            if (data.exito === 1) {
              loadPizzas();
            } else {
              alert('Error al eliminar: ' + data.mensaje);
            }
          })
          .catch(error => {
            console.error('Error en DELETE:', error);
            alert('Ocurrió un error al eliminar el registro. Revisa la consola para más detalles.');
          });
      }
    }
  
    window.initializePizzaModule = initializePizzaModule;
    window.editPizza = editPizza;
    window.deletePizza = deletePizza;
  })();
  