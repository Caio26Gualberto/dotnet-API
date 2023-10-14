import React from 'react';
import { useAlert } from '../../context/PopupContext'; // Importe o uso do contexto
import Alert from '@mui/material/Alert';
import Stack from '@mui/material/Stack';
import Style from './PopupMessages.module.css';

const PopupMessages: React.FC = () => {
  const { message, hideAlert } = useAlert(); // Use o contexto de mensagens

  if (!message) {
    return null; // Não renderiza se não houver mensagem
  }

  const { type, text } = message;

  const handleClose = () => {
    hideAlert();
  };

  return (
    <Stack sx={{ width: '100%' }} spacing={2} className={Style.popup_alert}>
      <Alert severity={type} onClose={handleClose}>
        {text}
      </Alert>
    </Stack>
  );
};

export default PopupMessages;
