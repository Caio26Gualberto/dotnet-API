import {BrowserRouter, Routes as Rotas, Route} from 'react-router-dom'
import LoginAndRegister from '../pages/LoginAndRegister'

const Routes = () => {
	return (
		<BrowserRouter>
		<Rotas>
			<Route path='/' element={<LoginAndRegister/>}/>
		</Rotas>
		</BrowserRouter>
	)
}

export default Routes