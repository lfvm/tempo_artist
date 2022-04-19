
const logOut = (e) => {
    
    e.preventDefault();
    //Borrar los datos guardaos 

    localStorage.removeItem('mail');
    localStorage.removeItem('nombre');
    localStorage.removeItem('apellidos');
    localStorage.removeItem('id');

    //navegar a inicio
    window.location.href = '/login';

}