import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
   vus: 10, // Numero de usuarios 
   duration: '10s' // Duração do teste de carga
};

export default function () {
    const res = http.get('http://localhost:8080/api/Clientes/cliente-padrao');
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
    sleep(1);
}