
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


const setInputValues = () => {

    const mail = localStorage.getItem('mail');
    const name = localStorage.getItem('nombre');
    const lastName = localStorage.getItem('apellidos');

    document.getElementById('title').innerHTML = name + " " + lastName;
    document.getElementById('name_input').value = name;
    document.getElementById('last_input').value = lastName;
    document.getElementById('email_input').value = mail



}