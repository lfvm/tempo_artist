const checkAuth = (e) => {
    //Si se encuentran los datos de un usuario autenticado quedarse en home, de lo contrario
    //ir a login
    e.preventDefault();
    const mail = localStorage.getItem('mail') ?? '';
    
    if(mail == ''){
        location.href = '/login';

    }

}
