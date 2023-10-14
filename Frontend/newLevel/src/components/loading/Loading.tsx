import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import Style from './Loading.module.css'
import { useContext } from 'react';
import { LoadingContext } from '../../context/LoadingContext';

const Loading = () => {
  const { isLoading } : any = useContext(LoadingContext);

  return (
    isLoading ? (
        <div className={Style.loading_overlay}>
          <Box
            sx={{
              display: 'flex',
              justifyContent: 'center',
              alignItems: 'center',
            }}
          >
            <CircularProgress className={Style.loading_indicator} />
          </Box>
        </div>
      ) : null
  )
}

export default Loading