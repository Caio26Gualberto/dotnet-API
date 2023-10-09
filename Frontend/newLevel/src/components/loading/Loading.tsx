import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import Style from './Loading.module.css'

const Loading = () => {
  return (
    <div className={Style.loading_overlay}>
            <Box
                sx={{
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                }}
            >
                <CircularProgress className={Style.loading_indicator}/>
            </Box>
        </div>
  )
}

export default Loading