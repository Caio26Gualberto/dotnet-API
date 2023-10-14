import {BrowserRouter, Routes as Rotas, Route} from 'react-router-dom'
import Login from '../pages/loginAndRegister/Login'
import Navbar from '../components/navbar/Navbar'
import Footer from '../components/footer/Footer'
import { LoadingProvider } from '../context/LoadingContext'
import Loading from '../components/loading/Loading'
import { AlertProvider } from '../context/PopupContext'
import ForgotPassword from '../pages/forgotPassword/ForgotPassword'



const Routes = () => {
  return (
    <LoadingProvider>
      <AlertProvider>
      <BrowserRouter>
      <Loading />
        <Navbar/>
          <Rotas>
            <Route path='/' element={<Login/>}/>
            <Route path='forgotPassword' element={<ForgotPassword/>}/>
          </Rotas>
        <Footer/>
      </BrowserRouter>
      </AlertProvider>
    </LoadingProvider>
  )
}

export default Routes