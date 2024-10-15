import GetCliente from "./GetCliente.js";
import { group, sleep } from 'k6';

export default () => {
    group('Endpoint Get Cliente - SnackTech.API', () => {
        GetCliente();
    });

    sleep(1);
}