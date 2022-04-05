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
    localStorage.setItem('password', password);

    //Redirigir a home
    window.location.href = '/';

}