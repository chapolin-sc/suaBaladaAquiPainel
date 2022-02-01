
// Possibilita um modal com conteúdo dinâmico, no caso para mostrar os flyer da festas.
var exampleModal = document.getElementById('Model')
    exampleModal.addEventListener('show.bs.modal', function (event) {

    var button = event.relatedTarget
    var recipient = button.getAttribute('data-bs-whatever')
    var modalBodyInput = exampleModal.querySelector('.modal-body img')

    modalBodyInput.setAttribute('src', recipient)
})
