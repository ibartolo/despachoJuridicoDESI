/* ============================
           JAVASCRIPT PARA AUTENTICACIÓN
           ============================ */

$(document).ready(function () {
    // Variables de estado
    let isProcessing = false;

    // Referencias a elementos del DOM
    const $errorAlert = $('#errorAlert');
    const $successAlert = $('#successAlert');
    const $loginForm = $('#loginForm');
    const $googleLogin = $('#googleLogin');
    const $facebookLogin = $('#facebookLogin');
    const $recoveryModal = $('#recoveryModal');
    const $registerModal = $('#registerModal');
    const $forgotPassword = $('#forgotPassword');
    const $registerLink = $('#registerLink');

    // ========================
    // FUNCIONES DE UTILIDAD
    // ========================

    // Mostrar mensaje de error
    function showError(message) {
        $('#errorMessage').text(message);
        $errorAlert.show();
        $successAlert.hide();

        // Ocultar después de 5 segundos
        setTimeout(() => {
            $errorAlert.hide();
        }, 5000);
    }

    // Mostrar mensaje de éxito
    function showSuccess(message) {
        $('#successMessage').text(message);
        $successAlert.show();
        $errorAlert.hide();
    }

    // Mostrar/ocultar loader en botón
    function setButtonLoading($button, isLoading) {
        if (isLoading) {
            $button.addClass('btn-loading');
            $button.prop('disabled', true);
        } else {
            $button.removeClass('btn-loading');
            $button.prop('disabled', false);
        }
    }

    // Validar formato de email
    function isValidEmail(email) {
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(email);
    }

    // Simular redirección después de login exitoso
    function redirectToDashboard() {
        setTimeout(() => {
            window.location.href = '/Home/Index'; // Cambiar por la URL real del dashboard
        }, 1500);
    }

    // ========================
    // MANEJADORES DE EVENTOS
    // ========================

    // Inicio de sesión con formulario
    $loginForm.on('submit', function (e) {
        e.preventDefault();

        if (isProcessing) return;

        const email = $('#email').val().trim();
        const password = $('#password').val();
        const rememberMe = $('#rememberMe').is(':checked');

        // Validaciones básicas
        if (!email || !password) {
            showError('Por favor, completa todos los campos.');
            return;
        }

        if (!isValidEmail(email)) {
            showError('Por favor, ingresa un correo electrónico válido.');
            return;
        }

        if (password.length < 6) {
            showError('La contraseña debe tener al menos 6 caracteres.');
            return;
        }

        // Simular proceso de autenticación
        isProcessing = true;
        setButtonLoading($('#submitLogin'), true);

        //// Simular llamada al servidor (en un caso real sería una petición AJAX)
        //setTimeout(() => {
        //    // Simulación de credenciales válidas
        //    const validCredentials = (
        //        (email === 'admin@despachojuridico.com' && password === 'admin123') ||
        //        (email === 'abogado@ejemplo.com' && password === 'abogado123')
        //    );

        //    if (validCredentials) {
        //        showSuccess('¡Inicio de sesión exitoso! Redirigiendo al dashboard...');

        //        // Guardar preferencia de "recordar sesión" (simulado)
        //        if (rememberMe) {
        //            localStorage.setItem('rememberedEmail', email);
        //        } else {
        //            localStorage.removeItem('rememberedEmail');
        //        }

        //        // Redirigir al dashboard
        //        redirectToDashboard();
        //    } else {
        //        showError('Credenciales incorrectas. Por favor, verifica tu correo y contraseña.');
        //    }

        //    isProcessing = false;
        //    setButtonLoading($('#submitLogin'), false);
        //}, 1500);

        var parameter = {
            email: email,
            pass: password
        };

        PostMVC("/Home/AutenticacionUsuario", parameter, function (r) {
            if (r.Result.Successful) {
                showSuccess('¡Inicio de sesión exitoso! Redirigiendo al dashboard...');
                redirectToDashboard();
            }
            else {
                //alert(r.Result.SystemMessages[0].Message);
                showError(r.Result.SystemMessages[0].Message);
                isProcessing = false;
                setButtonLoading($('#submitLogin'), false);
            }
        });
    });

    // Inicio de sesión con Google
    $googleLogin.on('click', function () {
        if (isProcessing) return;

        isProcessing = true;
        setButtonLoading($googleLogin, true);

        // Simular autenticación con Google
        setTimeout(() => {
            showSuccess('¡Autenticación con Google exitosa! Redirigiendo...');
            redirectToDashboard();

            isProcessing = false;
            setButtonLoading($googleLogin, false);
        }, 2000);
    });

    // Inicio de sesión con Facebook
    $facebookLogin.on('click', function () {
        if (isProcessing) return;

        isProcessing = true;
        setButtonLoading($facebookLogin, true);

        // Simular autenticación con Facebook
        setTimeout(() => {
            showSuccess('¡Autenticación con Facebook exitosa! Redirigiendo...');
            redirectToDashboard();

            isProcessing = false;
            setButtonLoading($facebookLogin, false);
        }, 2000);
    });

    // Mostrar modal de recuperación de contraseña
    $forgotPassword.on('click', function (e) {
        e.preventDefault();
        $recoveryModal.show();
        $('#recoveryEmail').focus();
    });

    // Mostrar modal de registro
    $registerLink.on('click', function (e) {
        e.preventDefault();
        $registerModal.show();
        $('#registerName').focus();
    });

    // Cerrar modal de recuperación
    $('#cancelRecovery').on('click', function () {
        $recoveryModal.hide();
        $('#recoverySuccess').hide();
        $('#recoveryError').hide();
        $('#recoveryEmail').val('');
    });

    // Cerrar modal de registro
    $('#cancelRegister').on('click', function () {
        $registerModal.hide();
        $('#registerSuccess').hide();
        $('#registerError').hide();
        $('#registerForm')[0].reset();
    });

    // Enviar enlace de recuperación
    $('#sendRecovery').on('click', function () {
        const email = $('#recoveryEmail').val().trim();

        if (!email) {
            $('#recoveryErrorMessage').text('Por favor, ingresa tu correo electrónico.');
            $('#recoveryError').show();
            return;
        }

        if (!isValidEmail(email)) {
            $('#recoveryErrorMessage').text('Por favor, ingresa un correo electrónico válido.');
            $('#recoveryError').show();
            return;
        }

        // Simular envío de correo
        $('#recoveryError').hide();
        $('#recoverySuccess').show();

        // Ocultar mensaje después de 3 segundos
        setTimeout(() => {
            $recoveryModal.hide();
            $('#recoverySuccess').hide();
            $('#recoveryEmail').val('');

            // Mostrar mensaje en el formulario principal
            showSuccess('Se ha enviado un enlace de recuperación a tu correo.');
        }, 3000);
    });

    // Enviar formulario de registro
    $('#submitRegister').on('click', function () {
        const name = $('#registerName').val().trim();
        const email = $('#registerEmail').val().trim();
        const password = $('#registerPassword').val();
        const confirmPassword = $('#registerConfirmPassword').val();
        const acceptTerms = $('#acceptTerms').is(':checked');

        // Validaciones
        if (!name || !email || !password || !confirmPassword) {
            $('#registerErrorMessage').text('Por favor, completa todos los campos.');
            $('#registerError').show();
            return;
        }

        if (!isValidEmail(email)) {
            $('#registerErrorMessage').text('Por favor, ingresa un correo electrónico válido.');
            $('#registerError').show();
            return;
        }

        if (password.length < 8) {
            $('#registerErrorMessage').text('La contraseña debe tener al menos 8 caracteres.');
            $('#registerError').show();
            return;
        }

        if (password !== confirmPassword) {
            $('#registerErrorMessage').text('Las contraseñas no coinciden.');
            $('#registerError').show();
            return;
        }

        if (!acceptTerms) {
            $('#registerErrorMessage').text('Debes aceptar los términos y condiciones.');
            $('#registerError').show();
            return;
        }

        // Simular registro exitoso
        $('#registerError').hide();
        $('#registerSuccess').show();

        // Simular creación de cuenta y redirección
        setTimeout(() => {
            $registerModal.hide();
            $('#registerSuccess').hide();
            $('#registerForm')[0].reset();

            // Mostrar mensaje en el formulario principal
            showSuccess('¡Cuenta creada exitosamente! Inicia sesión con tus nuevas credenciales.');

            // Autocompletar el email en el formulario de login
            $('#email').val(email);
            $('#password').focus();
        }, 2000);
    });

    // Cerrar modales al hacer clic fuera
    $(document).on('click', function (e) {
        if ($(e.target).is('.modal-overlay')) {
            $('.modal-overlay').hide();
            $('#recoverySuccess').hide();
            $('#recoveryError').hide();
            $('#registerSuccess').hide();
            $('#registerError').hide();

            // Resetear formularios
            $('#recoveryEmail').val('');
            $('#registerForm')[0].reset();
        }
    });

    // Cerrar modales con tecla Escape
    $(document).on('keyup', function (e) {
        if (e.key === 'Escape') {
            $('.modal-overlay').hide();
            $('#recoverySuccess').hide();
            $('#recoveryError').hide();
            $('#registerSuccess').hide();
            $('#registerError').hide();

            // Resetear formularios
            $('#recoveryEmail').val('');
            $('#registerForm')[0].reset();
        }
    });

    // ========================
    // INICIALIZACIÓN
    // ========================

    // Verificar si hay un email recordado
    const rememberedEmail = localStorage.getItem('rememberedEmail');
    if (rememberedEmail) {
        $('#email').val(rememberedEmail);
        $('#rememberMe').prop('checked', true);
        $('#password').focus();
    } else {
        $('#email').focus();
    }

    // Mostrar mensaje de bienvenida en consola
    console.log('Sistema de autenticación cargado. Bienvenido al Despacho Jurídico.');
});