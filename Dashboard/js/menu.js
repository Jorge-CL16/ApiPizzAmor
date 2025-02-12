const apiMenuUrl  = 'https://localhost:44370/api/Menu';
    const apiSizesUrl = 'https://localhost:44370/api/TamañoDePizza';
    const apiTypesUrl = 'https://localhost:44370/api/TiposDePizza';

    let sizes = [];
    let types = [];

    function loadSizes() {
      fetch(apiSizesUrl)
        .then(response => response.json())
        .then(data => {
          if (data.exito === 1) {
            sizes = data.data;
            populateSizeSelects();
            loadMenus();
          } else {
            alert('Error al cargar tamaños: ' + data.mensaje);
          }
        })
        .catch(error => console.error('Error al cargar tamaños:', error));
    }

    function loadTypes() {
      fetch(apiTypesUrl)
        .then(response => response.json())
        .then(data => {
          if (data.exito === 1) {
            types = data.data;
            populateTypeSelects();
            loadMenus();
          } else {
            alert('Error al cargar tipos: ' + data.mensaje);
          }
        })
        .catch(error => console.error('Error al cargar tipos:', error));
    }

    function populateTypeSelects() {
      const addTypeSelect = document.getElementById('idTipoPizza');
      const editTypeSelect = document.getElementById('editIdTipoPizza');
      addTypeSelect.innerHTML = '';
      editTypeSelect.innerHTML = '';

      types.forEach(type => {
        const option = document.createElement('option');
        option.value = type.idTipoPizza;
        option.textContent = type.nombre;
        addTypeSelect.appendChild(option);
        editTypeSelect.appendChild(option.cloneNode(true));
      });
    }

    function populateSizeSelects() {
      const addSizeSelect = document.getElementById('idTamano');
      const editSizeSelect = document.getElementById('editIdTamano');
      addSizeSelect.innerHTML = '';
      editSizeSelect.innerHTML = '';
      sizes.forEach(size => {
        const option = document.createElement('option');
        option.value = size.idTamano;
        option.textContent = `${size.nombre} - $${size.precio.toFixed(2)}`;
        addSizeSelect.appendChild(option);
        editSizeSelect.appendChild(option.cloneNode(true));
      });
    }

    function loadMenus() {
      fetch(apiMenuUrl)
        .then(response => response.json())
        .then(data => {
          if (data.exito === 1) {
            const menus = data.data;
            const tbody = document.querySelector('#menuTable tbody');
            tbody.innerHTML = '';

            menus.forEach(menu => {
              const typeObj = types.find(t => t.idTipoPizza === menu.idTipoPizza);
              const sizeObj = sizes.find(s => s.idTamano === menu.idTamano);
              const typeName = typeObj ? typeObj.nombre : menu.idTipoPizza;
              const sizeName = sizeObj ? sizeObj.nombre : menu.idTamano;

              const precioMostrar = sizeObj ? sizeObj.precio.toFixed(2) : menu.precio;

              const tr = document.createElement('tr');
              tr.innerHTML = `
                <td>${menu.idMenu}</td>
                <td>${menu.nombre}</td>
                <td>${menu.descripcion}</td>
                <td><img src="${menu.imagenUrl}" alt="${menu.nombre}" style="width:50px; height:auto;"></td>
                <td>${precioMostrar}</td>
                <td>${typeName}</td>
                <td>${sizeName}</td>
                <td>
                  <button class="btn btn-sm btn-warning" onclick="showEditModal(${menu.idMenu})">
                    Editar
                  </button>
                  <button class="btn btn-sm btn-danger" onclick="deleteMenu(${menu.idMenu})">
                    Eliminar
                  </button>
                </td>
              `;
              tbody.appendChild(tr);
            });
          } else {
            alert('Error al cargar menús: ' + data.mensaje);
          }
        })
        .catch(error => console.error('Error al cargar menús:', error));
    }

    document.getElementById('idTamano').addEventListener('change', function() {
      const selectedId = parseInt(this.value);
      const selectedSize = sizes.find(s => s.idTamano === selectedId);
      if (selectedSize) {
        document.getElementById('precio').value = selectedSize.precio;
      }
    });

    document.getElementById('editIdTamano').addEventListener('change', function() {
      const selectedId = parseInt(this.value);
      const selectedSize = sizes.find(s => s.idTamano === selectedId);
      if (selectedSize) {
        document.getElementById('editPrecio').value = selectedSize.precio;
      }
    });

    document.getElementById('addForm').addEventListener('submit', function(e) {
      e.preventDefault();

      const menu = {
        
        nombre: document.getElementById('nombre').value,
        descripcion: document.getElementById('descripcion').value,
        imagenUrl: document.getElementById('imagenUrl').value,
        idTipoPizza: parseInt(document.getElementById('idTipoPizza').value),
        idTamano: parseInt(document.getElementById('idTamano').value)
      };

      fetch(apiMenuUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(menu)
      })
      .then(response => response.json())
      .then(data => {
        if (data.exito === 1) {
          bootstrap.Modal.getInstance(document.getElementById('addModal')).hide();
          document.getElementById('addForm').reset();
          loadMenus();
        } else {
          alert('Error al agregar menú: ' + data.mensaje);
        }
      })
      .catch(error => console.error('Error al agregar menú:', error));
    });

    function showEditModal(idMenu) {
      fetch(apiMenuUrl)
        .then(response => response.json())
        .then(data => {
          if (data.exito === 1) {
            const menu = data.data.find(m => m.idMenu === idMenu);
            if (menu) {
              document.getElementById('editIdMenu').value = menu.idMenu;
              document.getElementById('editNombre').value = menu.nombre;
              document.getElementById('editDescripcion').value = menu.descripcion;
              document.getElementById('editImagenUrl').value = menu.imagenUrl;
              document.getElementById('editIdTipoPizza').value = menu.idTipoPizza;
              document.getElementById('editIdTamano').value = menu.idTamano;
              
              const selectedSize = sizes.find(s => s.idTamano === menu.idTamano);
              if (selectedSize) {
                document.getElementById('editPrecio').value = selectedSize.precio;
              } else {
                document.getElementById('editPrecio').value = menu.precio;
              }
              
              new bootstrap.Modal(document.getElementById('editModal')).show();
            }
          } else {
            alert('Error al cargar el menú para editar: ' + data.mensaje);
          }
        })
        .catch(error => console.error('Error al mostrar modal de edición:', error));
    }

    document.getElementById('editForm').addEventListener('submit', function(e) {
      e.preventDefault();

      const menu = {
        idMenu: parseInt(document.getElementById('editIdMenu').value),
        nombre: document.getElementById('editNombre').value,
        descripcion: document.getElementById('editDescripcion').value,
        imagenUrl: document.getElementById('editImagenUrl').value,
        precio: parseFloat(document.getElementById('editPrecio').value),
        idTipoPizza: parseInt(document.getElementById('editIdTipoPizza').value),
        idTamano: parseInt(document.getElementById('editIdTamano').value)
      };

      fetch(apiMenuUrl, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(menu)
      })
      .then(response => response.json())
      .then(data => {
        if (data.exito === 1) {
          bootstrap.Modal.getInstance(document.getElementById('editModal')).hide();
          loadMenus();
        } else {
          alert('Error al actualizar menú: ' + data.mensaje);
        }
      })
      .catch(error => console.error('Error al actualizar menú:', error));
    });

    function deleteMenu(idMenu) {
      if (confirm('¿Está seguro de eliminar este menú?')) {
        fetch(apiMenuUrl + '/' + idMenu, {
          method: 'DELETE'
        })
        .then(response => response.json())
        .then(data => {
          if (data.exito === 1) {
            loadMenus();
          } else {
            alert('Error al eliminar menú: ' + data.mensaje);
          }
        })
        .catch(error => console.error('Error al eliminar menú:', error));
      }
    }

    function updateImagePreview(input, imageId) {
  const imagePreview = document.getElementById(imageId);
  if (input.value) {
    imagePreview.src = input.value;
    imagePreview.style.display = 'block';
  } else {
    imagePreview.style.display = 'none';
  }
}

function showEditModal(idMenu) {
  fetch(apiMenuUrl)
    .then(response => response.json())
    .then(data => {
      if (data.exito === 1) {
        const menu = data.data.find(m => m.idMenu === idMenu);
        if (menu) {
          document.getElementById('editIdMenu').value = menu.idMenu;
          document.getElementById('editNombre').value = menu.nombre;
          document.getElementById('editDescripcion').value = menu.descripcion;
          document.getElementById('editImagenUrl').value = menu.imagenUrl;
          document.getElementById('editIdTipoPizza').value = menu.idTipoPizza;
          document.getElementById('editIdTamano').value = menu.idTamano;

          const editImagePreview = document.getElementById('editImagePreview');
          editImagePreview.src = menu.imagenUrl;
          editImagePreview.style.display = 'block';

          new bootstrap.Modal(document.getElementById('editModal')).show();
        }
      } else {
        alert('Error al cargar el menú para editar: ' + data.mensaje);
      }
    })
    .catch(error => console.error('Error al mostrar modal de edición:', error));
}

    document.addEventListener('DOMContentLoaded', () => {
      loadSizes();
      loadTypes();
      loadMenus();
    });