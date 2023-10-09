import {BrowserRouter, Routes as Rotas, Route} from 'react-router-dom'
import Login from '../pages/loginAndRegister/Login'
import Navbar from '../components/navbar/Navbar'
import Footer from '../components/footer/Footer'
import PopupMessages from '../components/popupMessages/PopupMessages'

const Routes = () => {
  return (
    <BrowserRouter>
      <Navbar/>
      {/* <PopupMessages messageType='error' text='Onde ta'/> */}
        <Rotas>
          <Route path='/' element={<Login/>}/>
        </Rotas>
      <Footer/>
    </BrowserRouter>
  )
}

export default Routes