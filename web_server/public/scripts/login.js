//Scripts para manejar la pantalla del login 



const handleLogin = async(e) => {

    e.preventDefault();

    const loginForm = document.getElementById('login_form');

    const email = loginForm[0].value;
    const password = loginForm[1].value;

    if (email === '' || password === '') {
        return alert('Please fill in all fields');
    }

    //Guardar los datos en el localStorage
    localStorage.setItem('mail', email);


    //TODO: Enviar los datos al servidor para validar, en caso de ser correctos ir a home, de lo contrario mostrar mensaje de error
    

    //Redirigir a home
    window.location.href = '/';

}