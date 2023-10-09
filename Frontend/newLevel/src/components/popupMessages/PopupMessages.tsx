import React, { useState, useEffect } from 'react';
import Alert from '@mui/material/Alert';
import Stack from '@mui/material/Stack';
import Style from './PopupMessages.module.css'

const PopupMessages: React.FC<{ messageType: string, text: string }> = ({ messageType, text }) => {
    return (
        <Stack sx={{ width: '100%' }} spacing={2} className={Style.popup_alert}>         
                <Alert severity={messageType as any}>
                    {text}
                </Alert>         
        </Stack>
    )
}

export default PopupMessages;
