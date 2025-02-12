const apiBase = 'https://localhost:44370/api';

let menuItems = [];
let refrescos = [];

function initializeOrdenModule() {
  cargarClientesOrden();
  cargarEmpleadosOrden();
  cargarMenuOrden();
  cargarRefrescosAlmacenOrden();

  const orderForm = document.getElementById('orderForm');
  if (orderForm) {
    orderForm.addEventListener('submit', guardarOrden);
  } else {
    console.error("No se encontró el elemento 'orderForm'.");
  }
}

function cargarClientesOrden() {
  fetch(`${apiBase}/Cliente`)
    .then(response => response.json())
    .then(data => {
      const clientes = data.data;
      const clienteSelect = document.getElementById('clienteSelect');
      if (clienteSelect) {
        clienteSelect.innerHTML = '<option value="">Seleccione un cliente</option>';
        clientes.forEach(c => {
          clienteSelect.innerHTML += `<option value="${c.idCliente}">${c.nombre}</option>`;
        });
      } else {
        console.error("No se encontró el elemento 'clienteSelect'.");
      }
    })
    .catch(error => console.error('Error al cargar clientes:', error));
}

function cargarEmpleadosOrden() {
  fetch(`${apiBase}/Empleado`)
    .then(response => response.json())
    .then(data => {
      const empleados = data.data;
      const empleadoSelect = document.getElementById('empleadoSelect');
      if (empleadoSelect) {
        empleadoSelect.innerHTML = '<option value="">Seleccione un empleado</option>';
        empleados.forEach(e => {
          empleadoSelect.innerHTML += `<option value="${e.idEmpleado}">${e.nombre} ${e.apellido}</option>`;
        });
      } else {
        console.error("No se encontró el elemento 'empleadoSelect'.");
      }
    })
    .catch(error => console.error('Error al cargar empleados:', error));
}

function cargarMenuOrden() {
  fetch(`${apiBase}/Menu`)
    .then(response => response.json())
    .then(data => {
      menuItems = data.data;
    })
    .catch(error => {
      console.error('Error al cargar el menú:', error);
      menuItems = [
        { idMenu: 1, nombre: "Pizza Hawaiana", precio: 8.50 },
        { idMenu: 2, nombre: "Pizza Pepperoni", precio: 10.00 }
      ];
    });
}

function cargarRefrescosAlmacenOrden() {
  fetch(`${apiBase}/Refrescos`)
    .then(response => response.json())
    .then(data => {
      refrescos = data.data;
    })
    .catch(error => console.error('Error al cargar refrescos:', error));
}

function agregarDetalle() {
  const tbody = document.querySelector('#detallesTable tbody');
  if (!tbody) {
    console.error("No se encontró el tbody de la tabla de detalles.");
    return;
  }
  const row = document.createElement('tr');

  row.innerHTML = `
    <td>
      <select class="form-select menuSelect">
        <option value="">Seleccione un menú</option>
        ${menuItems.map(item => `<option value="${item.idMenu}" data-precio="${item.precio}">${item.nombre}</option>`).join('')}
      </select>
    </td>
    <td>
      <input type="number" min="1" value="1" class="form-control cantidadInput" style="width:80px;">
    </td>
    <td>
      <input type="text" readonly class="form-control precioInput" style="width:100px;">
    </td>
    <td>
      <input type="text" readonly class="form-control subtotalInput" style="width:100px;" value="0.00">
    </td>
    <td>
      <button type="button" class="btn btn-danger btn-sm" onclick="eliminarFila(this)">Eliminar</button>
    </td>
  `;
  tbody.appendChild(row);

  const menuSelect = row.querySelector('.menuSelect');
  const cantidadInput = row.querySelector('.cantidadInput');

  menuSelect.addEventListener('change', function() {
    const selectedOption = this.options[this.selectedIndex];
    const precio = parseFloat(selectedOption.getAttribute('data-precio')) || 0;
    row.querySelector('.precioInput').value = precio.toFixed(2);
    actualizarSubtotalRow(row);
  });
  cantidadInput.addEventListener('input', function() {
    actualizarSubtotalRow(row);
  });
}

function agregarRefresco() {
  const tbody = document.querySelector('#refrescosTable tbody');
  if (!tbody) {
    console.error("No se encontró el tbody de la tabla de refrescos.");
    return;
  }
  const row = document.createElement('tr');

  row.innerHTML = `
    <td>
      <select class="form-select refrescoSelect">
        <option value="">Seleccione un refresco</option>
        ${refrescos.map(r => `<option value="${r.idRefresco}" data-precio="${r.precio}">${r.nombre}</option>`).join('')}
      </select>
    </td>
    <td>
      <input type="number" min="1" value="1" class="form-control cantidadInput" style="width:80px;">
    </td>
    <td>
      <input type="text" readonly class="form-control precioInput" style="width:100px;">
    </td>
    <td>
      <input type="text" readonly class="form-control subtotalInput" style="width:100px;" value="0.00">
    </td>
    <td>
      <button type="button" class="btn btn-danger btn-sm" onclick="eliminarFila(this)">Eliminar</button>
    </td>
  `;
  tbody.appendChild(row);

  const refrescoSelect = row.querySelector('.refrescoSelect');
  const cantidadInput = row.querySelector('.cantidadInput');

  refrescoSelect.addEventListener('change', function() {
    const selectedOption = this.options[this.selectedIndex];
    const precio = parseFloat(selectedOption.getAttribute('data-precio')) || 0;
    row.querySelector('.precioInput').value = precio.toFixed(2);
    actualizarSubtotalRow(row);
  });
  cantidadInput.addEventListener('input', function() {
    actualizarSubtotalRow(row);
  });
}

function eliminarFila(button) {
  const row = button.closest('tr');
  row.remove();
  actualizarTotal();
}

function actualizarSubtotalRow(row) {
  const cantidad = parseFloat(row.querySelector('.cantidadInput').value) || 0;
  const precio = parseFloat(row.querySelector('.precioInput').value) || 0;
  const subtotal = cantidad * precio;
  row.querySelector('.subtotalInput').value = subtotal.toFixed(2);
  actualizarTotal();
}

function actualizarTotal() {
  let total = 0;
  document.querySelectorAll('#detallesTable .subtotalInput').forEach(input => {
    total += parseFloat(input.value) || 0;
  });
  document.querySelectorAll('#refrescosTable .subtotalInput').forEach(input => {
    total += parseFloat(input.value) || 0;
  });
  const totalDisplay = document.getElementById('montoTotalDisplay');
  if (totalDisplay) {
    totalDisplay.textContent = total.toFixed(2);
  } else {
    console.error("No se encontró el elemento 'montoTotalDisplay'.");
  }
}

function guardarOrden(event) {
  event.preventDefault();
  
  const idCliente = document.getElementById('clienteSelect').value;
  const idEmpleado = document.getElementById('empleadoSelect').value;
  const fechaOrden = document.getElementById('fechaOrden').value;
  const montoTotal = parseFloat(document.getElementById('montoTotalDisplay').textContent);
  const fechaActual = new Date().toISOString();

  const orden = {
    idCliente: parseInt(idCliente),
    idEmpleado: parseInt(idEmpleado),
    fechaOrden: fechaOrden,
    montoTotal: montoTotal,
    creadoEn: fechaActual,
    actualizadoEn: fechaActual
  };

  fetch(`${apiBase}/Ordenes`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(orden)
  })
  .then(response => response.json())
  .then(data => {
    if (data.exito === 1) {
      document.getElementById('ticketDetails').innerHTML = `
        <h4>Ticket de Venta</h4>
        <p>Total: $${montoTotal.toFixed(2)}</p>
        <p>Fecha: ${new Date().toLocaleString()}</p>
      `;
      new bootstrap.Modal(document.getElementById('ticketModal')).show();

      // Reiniciamos el formulario y vaciamos las tablas de detalles y refrescos
      document.getElementById('orderForm').reset();
      document.querySelector('#detallesTable tbody').innerHTML = '';
      document.querySelector('#refrescosTable tbody').innerHTML = '';
      actualizarTotal();
    } else {
      alert('Error al guardar la orden: ' + data.mensaje);
    }
  })
  .catch(error => {
    console.error('Error al guardar la orden:', error);
    alert('Error al guardar la orden');
  });
}

