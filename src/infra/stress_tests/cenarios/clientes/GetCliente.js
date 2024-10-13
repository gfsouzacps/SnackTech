import http from 'k6/http';
import { sleep } from 'k6';
import { Trend, Rate, Counter } from "k6/metrics"
import { check, fail } from "k6";

export let GetClienteDuration = new Trend('get_cliente_duration');
export let GetClienteFailRate = new Rate('get_cliente_fail_rate');
export let GetClienteSuccessRate = new Rate('get_cliente_success_rate');
export let GetClienteReqs = new Rate('get_cliente_reqs');

export default function () {
    let res = http.get('http://localhost:8080/api/Clientes/13220565794');

    GetClienteDuration.add(res.timings.duration);
    GetClienteReqs.add(1);
    GetClienteFailRate.add(res.status == 0 || res.status > 399);
    GetClienteSuccessRate.add(res.status < 399);

    // let durationMsg = 'Max Duration ${4000/1000}s'
    // if(!check(res, {
    //     'max duration': (r) => r.timings.duration < 4000,
    // })){
    //     fail(durationMsg);
    // }

    sleep(1);
}