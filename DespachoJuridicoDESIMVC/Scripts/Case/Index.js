/* ========================================
   JAVASCRIPT ESPECÍFICO PARA VENTANA DE CASOS
   ======================================== */

$(document).ready(function () {
    // Variables de estado para la ventana de casos
    let modoEdicion = false;
    let casoId = null;
    let clienteSeleccionado = null;
    let clientesData = [];
    let estatusCasoData = [];

    // ========================
    // FUNCIONES DE UTILIDAD
    // ========================

    // Mostrar/ocultar loading overlay
    function showLoading(show) {
        if (show) {
            $('#loadingOverlay').show();
        } else {
            $('#loadingOverlay').hide();
        }
    }

    // Mostrar mensaje de validación
    function showValidationMessage(elementId, message, isError = true) {
        const $message = $(`#${elementId}Error`);
        $message.text(message);
        $message.removeClass('success').addClass(isError ? 'error' : 'success');
        $message.show();

        // Resaltar campo con error
        if (isError) {
            $(`#${elementId}`).addClass('error-field');
        }
    }

    // Ocultar mensaje de validación
    function hideValidationMessage(elementId) {
        $(`#${elementId}Error`).hide();
        $(`#${elementId}`).removeClass('error-field');
    }

    // Validar formulario
    function validarFormulario() {
        let valido = true;

        // Validar número de caso
        const numeroCaso = $('#numeroCaso').val().trim();
        if (!numeroCaso) {
            showValidationMessage('numeroCaso', 'El número de caso es obligatorio');
            valido = false;
        } else {
            hideValidationMessage('numeroCaso');
        }

        // Validar cliente
        const clienteId = $('#clienteId').val();
        if (!clienteId) {
            showValidationMessage('clienteId', 'Debe seleccionar un cliente');
            valido = false;
        } else {
            hideValidationMessage('clienteId');
        }

        // Validar estatus del caso
        const estatusCasoId = $('#estatusCasoId').val();
        if (!estatusCasoId) {
            showValidationMessage('estatusCasoId', 'Debe seleccionar un estatus');
            valido = false;
        } else {
            hideValidationMessage('estatusCasoId');
        }

        // Validar monto inicial
        const montoInicial = $('#montoInicial').val();
        if (!montoInicial || parseFloat(montoInicial) <= 0) {
            showValidationMessage('montoInicial', 'El monto inicial debe ser mayor a 0');
            valido = false;
        } else {
            hideValidationMessage('montoInicial');
        }

        return valido;
    }

    // Limpiar formulario
    function limpiarFormulario() {
        $('#numeroCaso').val('');
        $('#clienteId').val('');
        $('#estatusCasoId').val('');
        $('#descripcion').val('');
        $('#montoInicial').val('');
        $('#estatus').prop('checked', true);
        $('#estatusLabel').text('Activo');

        // Ocultar sección de auditoría
        $('#seccionAuditoria').hide();
        $('.validation-message').hide();
        $('.form-control').removeClass('error-field');

        // Resetear variables
        casoId = null;
        clienteSeleccionado = null;
        modoEdicion = false;

        // Ocultar botón eliminar
        $('#btnEliminar').hide();

        // Generar número de caso automático
        generarNumeroCaso();

        // Enfocar primer campo
        $('#numeroCaso').focus();
    }

    // Generar número de caso automático
    function generarNumeroCaso() {
        const fecha = new Date();
        const year = fecha.getFullYear();
        const month = String(fecha.getMonth() + 1).padStart(2, '0');
        const day = String(fecha.getDate()).padStart(2, '0');

        // Generar número secuencial (en un caso real vendría del servidor)
        const randomNum = Math.floor(Math.random() * 999) + 1;
        const numeroCaso = `C-${year}-${month}${day}-${String(randomNum).padStart(3, '0')}`;

        $('#numeroCaso').val(numeroCaso);
    }

    // Cargar combobox de clientes
    function cargarClientes() {
        // Simular datos de clientes (en un caso real sería una petición AJAX)
        clientesData = [
            { id: 1, nombre: 'María González Rodríguez', email: 'maria.gonzalez@email.com', telefono: '555-123-4567' },
            { id: 2, nombre: 'Carlos Ruiz Martínez', email: 'carlos.ruiz@email.com', telefono: '555-234-5678' },
            { id: 3, nombre: 'Ana López Sánchez', email: 'ana.lopez@email.com', telefono: '555-345-6789' },
            { id: 4, nombre: 'Pedro Hernández García', email: 'pedro.hernandez@email.com', telefono: '555-456-7890' },
            { id: 5, nombre: 'Laura Díaz Fernández', email: 'laura.diaz@email.com', telefono: '555-567-8901' }
        ];

        const $clienteSelect = $('#clienteId');
        $clienteSelect.empty();
        $clienteSelect.append('<option value="">Seleccionar cliente...</option>');

        clientesData.forEach(cliente => {
            $clienteSelect.append(`<option value="${cliente.id}">${cliente.nombre}</option>`);
        });
    }

    // Cargar combobox de estatus de caso
    function cargarEstatusCaso() {
        // Simular datos de estatus (en un caso real sería una petición AJAX)
        estatusCasoData = [
            { id: 1, nombre: 'En Proceso', descripcion: 'Caso activo en trámite' },
            { id: 2, nombre: 'En Espera', descripcion: 'Esperando documentación o información' },
            { id: 3, nombre: 'Cerrado', descripcion: 'Caso finalizado exitosamente' },
            { id: 4, nombre: 'Archivado', descripcion: 'Caso archivado por inactividad' },
            { id: 5, nombre: 'Suspendido', descripcion: 'Caso temporalmente suspendido' }
        ];

        const $estatusSelect = $('#estatusCasoId');
        $estatusSelect.empty();
        $estatusSelect.append('<option value="">Seleccionar estatus...</option>');

        estatusCasoData.forEach(estatus => {
            $estatusSelect.append(`<option value="${estatus.id}">${estatus.nombre}</option>`);
        });
    }

    // Cargar datos de un caso existente
    function cargarCaso(id) {
        showLoading(true);

        // Simular carga de datos (en un caso real sería una petición AJAX)
        setTimeout(() => {
            // Datos simulados de un caso
            const casoSimulado = {
                id: id,
                clienteId: 1,
                estatusCasoId: 1,
                numeroCaso: 'C-2023-1015-001',
                descripcion: 'Caso de divorcio con acuerdo de custodia compartida y división de bienes.',
                montoInicial: 15000.00,
                estatus: true,
                createdBy: 'Abogado Juan',
                createdDt: '2023-10-15 09:30:00',
                updatedBy: 'Abogado Juan',
                updatedDt: '2023-10-20 14:15:00'
            };

            // Llenar formulario con datos
            $('#numeroCaso').val(casoSimulado.numeroCaso);
            $('#clienteId').val(casoSimulado.clienteId);
            $('#estatusCasoId').val(casoSimulado.estatusCasoId);
            $('#descripcion').val(casoSimulado.descripcion);
            $('#montoInicial').val(casoSimulado.montoInicial);
            $('#estatus').prop('checked', casoSimulado.estatus);
            $('#estatusLabel').text(casoSimulado.estatus ? 'Activo' : 'Inactivo');

            // Actualizar contador de descripción
            $('#descripcionContador').text(casoSimulado.descripcion.length);

            // Mostrar información de auditoría
            $('#createdBy').text(casoSimulado.createdBy);
            $('#createdDt').text(casoSimulado.createdDt);
            $('#updatedBy').text(casoSimulado.updatedBy);
            $('#updatedDt').text(casoSimulado.updatedDt);
            $('#seccionAuditoria').show();

            // Actualizar variables de estado
            casoId = casoSimulado.id;
            modoEdicion = true;

            // Mostrar botón eliminar
            $('#btnEliminar').show();

            showLoading(false);

            console.log(`Caso ${id} cargado para edición`);
        }, 1000);
    }

    // Guardar caso (crear o actualizar)
    function guardarCaso() {
        if (!validarFormulario()) {
            return;
        }

        showLoading(true);

        // Preparar datos del formulario
        const casoData = {
            id: casoId,
            clienteId: $('#clienteId').val(),
            estatusCasoId: $('#estatusCasoId').val(),
            numeroCaso: $('#numeroCaso').val().trim(),
            descripcion: $('#descripcion').val().trim(),
            montoInicial: parseFloat($('#montoInicial').val()),
            estatus: $('#estatus').is(':checked')
        };

        // Simular envío al servidor (en un caso real sería una petición AJAX)
        setTimeout(() => {
            showLoading(false);

            // Mostrar mensaje de éxito
            const mensaje = modoEdicion
                ? `Caso "${casoData.numeroCaso}" actualizado correctamente.`
                : `Caso "${casoData.numeroCaso}" creado correctamente.`;

            alert(mensaje);

            // Si es nuevo caso, limpiar formulario
            if (!modoEdicion) {
                limpiarFormulario();
            } else {
                // Si es edición, recargar datos para mostrar auditoría actualizada
                cargarCaso(casoId);
            }

            console.log('Caso guardado:', casoData);
        }, 1500);
    }

    // Eliminar caso
    function eliminarCaso() {
        if (!casoId || !confirm('¿Está seguro de eliminar este caso? Esta acción no se puede deshacer.')) {
            return;
        }

        showLoading(true);

        // Simular eliminación (en un caso real sería una petición AJAX)
        setTimeout(() => {
            showLoading(false);
            alert(`Caso "${$('#numeroCaso').val()}" eliminado correctamente.`);
            limpiarFormulario();
            console.log(`Caso ${casoId} eliminado`);
        }, 1000);
    }

    // Buscar clientes para el modal
    function buscarClientes(termino) {
        const resultados = clientesData.filter(cliente => {
            const busqueda = termino.toLowerCase();
            return (
                cliente.nombre.toLowerCase().includes(busqueda) ||
                cliente.email.toLowerCase().includes(busqueda) ||
                cliente.telefono.includes(busqueda)
            );
        });

        const $resultados = $('#clientSearchResults');
        $resultados.empty();

        if (resultados.length === 0) {
            $resultados.html('<p class="text-center text-muted">No se encontraron clientes.</p>');
            $('#btnSelectClient').prop('disabled', true);
            return;
        }

        resultados.forEach(cliente => {
            const esSeleccionado = clienteSeleccionado && clienteSeleccionado.id === cliente.id;
            const clienteHTML = `
                        <div class="client-item ${esSeleccionado ? 'selected' : ''}" data-id="${cliente.id}">
                            <div class="client-name">${cliente.nombre}</div>
                            <div class="client-info">
                                <i class="fa fa-envelope"></i> ${cliente.email} | 
                                <i class="fa fa-phone"></i> ${cliente.telefono}
                            </div>
                        </div>
                    `;
            $resultados.append(clienteHTML);
        });

        // Habilitar botón de selección si hay un cliente seleccionado
        $('#btnSelectClient').prop('disabled', !clienteSeleccionado);
    }

    // ========================
    // MANEJADORES DE EVENTOS
    // ========================

    // Botón: Nuevo caso
    $('#btnNuevoCaso').click(function () {
        limpiarFormulario();
    });

    // Botón: Ver lista (simulación)
    $('#btnListaCasos').click(function () {
        alert('Aquí se mostraría la lista de casos. Esta funcionalidad se implementará en otra ventana.');
    });

    // Botón: Generar número de caso
    $('#btnGenerarNumero').click(function () {
        generarNumeroCaso();
    });

    // Botón: Buscar cliente
    $('#btnBuscarCliente').click(function () {
        $('#clientSearchModal').modal('show');
        $('#clientSearchInput').val('').focus();
        $('#clientSearchResults').empty();
        clienteSeleccionado = null;
        $('#btnSelectClient').prop('disabled', true);
    });

    // Botón: Buscar en modal de clientes
    $('#btnClientSearch').click(function () {
        const termino = $('#clientSearchInput').val().trim();
        if (termino) {
            buscarClientes(termino);
        }
    });

    // Input de búsqueda en modal de clientes (búsqueda al presionar Enter)
    $('#clientSearchInput').keypress(function (e) {
        if (e.which === 13) {
            $('#btnClientSearch').click();
            return false;
        }
    });

    // Seleccionar cliente en modal
    $(document).on('click', '.client-item', function () {
        const clienteId = $(this).data('id');
        clienteSeleccionado = clientesData.find(c => c.id == clienteId);

        // Resaltar selección
        $('.client-item').removeClass('selected');
        $(this).addClass('selected');

        // Habilitar botón de selección
        $('#btnSelectClient').prop('disabled', false);
    });

    // Botón: Seleccionar cliente en modal
    $('#btnSelectClient').click(function () {
        if (clienteSeleccionado) {
            $('#clienteId').val(clienteSeleccionado.id);
            $('#clientSearchModal').modal('hide');
            console.log('Cliente seleccionado:', clienteSeleccionado);
        }
    });

    // Botón: Guardar caso
    $('#btnGuardar').click(function () {
        guardarCaso();
    });

    // Botón: Cancelar
    $('#btnCancelar').click(function () {
        if (modoEdicion) {
            // Si está en modo edición, recargar datos originales
            cargarCaso(casoId);
        } else {
            // Si está en modo nuevo, limpiar formulario
            limpiarFormulario();
        }
    });

    // Botón: Eliminar
    $('#btnEliminar').click(function () {
        eliminarCaso();
    });

    // Contador de caracteres para descripción
    $('#descripcion').on('input', function () {
        const longitud = $(this).val().length;
        $('#descripcionContador').text(longitud);

        // Cambiar color si se acerca al límite
        const $contador = $('#descripcionContador');
        if (longitud > 450) {
            $contador.css('color', '#e74c3c');
        } else if (longitud > 400) {
            $contador.css('color', '#f39c12');
        } else {
            $contador.css('color', '#999');
        }
    });

    // Cambiar label del switch de estatus
    $('#estatus').change(function () {
        $('#estatusLabel').text($(this).is(':checked') ? 'Activo' : 'Inactivo');
    });

    // Simular carga de un caso existente al cargar la página (para demostración)
    $('#btnNuevoCaso').click(); // Limpiar formulario primero

    // ========================
    // INICIALIZACIÓN
    // ========================

    // Cargar datos iniciales
    cargarClientes();
    cargarEstatusCaso();

    // Para demostración, simular que estamos editando un caso existente
    // En una implementación real, esto dependería de la URL o parámetros
    setTimeout(() => {
        // Simular que el usuario quiere editar un caso existente (ID 1)
        // cargarCaso(1);
    }, 500);

    console.log('Ventana de casos inicializada correctamente.');
});