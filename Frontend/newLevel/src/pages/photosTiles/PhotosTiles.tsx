import { useAlert } from '../../context/PopupContext';


const PhotosTiles = () => {
    const { showAlert } = useAlert()

    const Caio = () => {
        showAlert('error', 'Caio')
    }

    return(
        <div>
        <a onClick={Caio}>Caio</a>
        </div>
    )
}

export default PhotosTiles