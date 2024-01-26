import React from 'react';
import Style from '../postLogin/postLogin.module.css'; // Certifique-se de ter o arquivo CSS para os estilos específicos
import Button from 'react-bootstrap/Button';

const PostLogin: React.FC = () => {
    return (
        <div className={Style.post_login_container}>
            <div className={Style.background_image}></div>
            <div className={Style.content}>
                <h1>Bem-vindo ao New Level</h1>
                <p>
                    Bem-vindo ao New Level, um espaço dedicado aos verdadeiros amantes do metal que viveram a intensidade dos anos 80 na região do ABC, mais especificamente em Santo André. Aqui, a paixão pela música pesada e as memórias de uma época única se encontram para celebrar a cultura headbanger.

                    Nossa história começa com as experiências marcantes de um jovem na cena metal daquela época. Meu pai, junto com seus companheiros headbangers, trilhava os caminhos do Celtic Frost, Slayer, Whiplash, Exodus e muitos outros ícones do metal. As histórias de brigas épicas e shows inesquecíveis eram compartilhadas, criando uma teia de conexões que se estendia além da música.

                    Hoje, mergulhado no mundo da programação, surgiu a ideia de criar este espaço para unir os headbangers das antigas. Este site, concebido sem grandes planejamentos, busca resgatar as raízes do metal e reacender as lembranças nostálgicas daquela época dourada. Mais do que um simples portal, é um convite para reviver as emoções, relembrar as vivências e conectar-se novamente com a cultura que moldou tantas histórias.

                    Explore nossas páginas e embarque em uma jornada pela essência do metal. Seja revivendo as lendárias batalhas de mosh pit, relembrando as letras que ecoavam pelas ruas do ABC, ou simplesmente compartilhando suas próprias histórias, New Level é o lugar onde a comunidade headbanger se encontra para celebrar o passado e construir novas conexões no presente.

                    Junte-se a nós, e vamos juntos resgatar as batidas intensas, as guitarras estridentes e a energia única que define o verdadeiro espírito do metal. New Level é mais do que um portal, é uma celebração da paixão que transcende gerações. \m/
                </p>
                <div className={Style.button}>
                    <Button variant="outline-light" size='lg'>Começar</Button>
                </div>
            </div>
        </div>
    );
};

export default PostLogin;
